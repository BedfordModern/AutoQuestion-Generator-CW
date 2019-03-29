using AutoQuestionGenerator.Accounts;
using AutoQuestionGenerator.DatabaseModels;
using AutoQuestionGenerator.Helper;
using AutoQuestionGenerator.Models.Statistics;
using System.Collections.Generic;
using System.Linq;

namespace AutoQuestionGenerator.Models
{
    public class GroupResultsViewModel
    {
        /// <summary>
        /// This will convert a workset into a number of question sets based on the user
        /// </summary>
        /// <param name="WorksetID"></param>
        /// <param name="_context"></param>
        public GroupResultsViewModel(int WorksetID)
        {
            //Get each question set that has been answered by the students in the group.
            var sets = DatabaseConnector.GetWhere<QuestionSets>($"WorkSetID = {WorksetID}").ToList();
            int correct = 0, total = 0;
            List<UserQuestions> usrSpecificSets = new List<UserQuestions>();
            List<CompletedQuestion> allQuestions = new List<CompletedQuestion>();

            var dbQuestionTypes = DatabaseConnector.Get<QuestionTypes>();

            //Itterates through each question set.
            foreach (var set in sets)
            {
                // Collect the list of questions for this set.
                var questionsDB = DatabaseConnector.GetWhere<Questions>($"QuestionSetID={set.QuestionSetID}").ToList();

                List<CompletedQuestion> questionsLists = new List<CompletedQuestion>();

                //Convert each question in the set into a program usable one.
                foreach (var question in questionsDB)
                {
                    questionsLists.Add(new CompletedQuestion()
                    {
                        Type = dbQuestionTypes.First(x => x.TypeID == question.Question_Type),
                        AnsweredCorrect = question.AnswerCorrect
                    });
                    total++;
                    if (question.AnswerCorrect > 0)
                        correct++;
                }

                //Add this question to the list of all questions completed by the students.
                allQuestions.AddRange(questionsLists);
                string name = "User not found / deleted";
                Users usr = UserHelper.GetUser(set.UserID);
                if (usr != null)
                {
                    name = usr.First_Name + " " + usr.Last_Name;
                }

                //See if we have already added a question set by this user.
                if (usrSpecificSets.FirstOrDefault(x => x.UserID == set.UserID) == null)
                {
                    //If not create a new one.
                    usrSpecificSets.Add(new UserQuestions()
                    {
                        UserID = set.UserID,
                        Name = name,
                        //Set the percentage scores (highest and lowest) to the current one.
                        WorstPercentage = new PercentageModel()
                        {
                            Current = questionsLists.Where(x => x.AnsweredCorrect > 0).Count(),
                            Total = questionsLists.Count()
                        },
                        Percentage = new PercentageModel()
                        {
                            Current = questionsLists.Where(x => x.AnsweredCorrect > 0).Count(),
                            Total = questionsLists.Count()
                        },
                        Questions = new CompletedQuestion[][] { questionsLists.ToArray() },
                        Attempts = 1,
                        NumberAnswered = questionsLists.Where(x => x.AnsweredCorrect != 0).Count()
                    });
                }
                else
                {
                    //Get the user's question information from the list.
                    UserQuestions userQuestionSet = usrSpecificSets.FirstOrDefault(x => x.UserID == set.UserID);
                    //Increase the number of attempt to the current number;
                    userQuestionSet.Attempts += 1;

                    //Add the new questions to the list of questions answered by the user.
                    var qrs = new List<CompletedQuestion[]>() { questionsLists.ToArray() };
                    qrs.AddRange(userQuestionSet.Questions);
                    userQuestionSet.Questions = qrs.ToArray();

                    //Calculate result as percentage score of the current question set and 
                    //check to see if it is higher or lower than other attempts.
                    var percent = new PercentageModel()
                    {
                        Current = questionsLists.Where(x => x.AnsweredCorrect > 0).Count(),
                        Total = questionsLists.Count()
                    };

                    if (userQuestionSet.Percentage.Percentage < percent.Percentage)
                    {
                        userQuestionSet.Percentage = percent;
                    }
                    else if (userQuestionSet.WorstPercentage.Percentage > percent.Percentage)
                    {
                        userQuestionSet.WorstPercentage = percent;
                    }

                    //Check if the number of questions answered is greater than before.
                    //If so, set the number of questions answered equal to this set's number of questions answered.
                    if (questionsLists.Where(x => x.AnsweredCorrect != 0).Count() > userQuestionSet.NumberAnswered)
                        userQuestionSet.NumberAnswered = questionsLists.Where(x => x.AnsweredCorrect != 0).Count();
                }
            }

            foreach (var item in usrSpecificSets)
            {
                // Grab all of the questions completed by the user and compile them into a single array.
                var allQuestionsByUser = new List<CompletedQuestion>();
                foreach (var list in item.Questions) { allQuestionsByUser.AddRange(list); }


                decimal bestper = 0.0m, worstper = 100.0m;

                // Group the questions by their type.
                foreach (var group in allQuestionsByUser.GroupBy(q => q.Type))
                {
                    // Count how many questions of a type the users got correct.
                    int correctForType = 0;
                    foreach (var question in group)
                    {
                        if (question.AnsweredCorrect > 0)
                        {
                            correctForType++;
                        }
                    }

                    // Calculate the percentage of this type which were correct.
                    decimal percent = ((decimal)correctForType / (decimal)group.Count()) * 100.0m;

                    // Check if the highest and lowest percentages are different to the current values,
                    // and set them to the new value if they are.
                    if (percent > bestper)
                    {
                        bestper = percent;
                        item.bestType = group.Key.Type_Name + " @ " + bestper.ToString("0.###") + "%";
                    }
                    else if (percent < worstper)
                    {
                        worstper = percent;
                        item.worstType = group.Key.Type_Name + " @ " + worstper.ToString("0.###") + "%";
                    }
                }
            }

            if (sets.Count != 0)
            {
                // Calculates where the student sits on a distribution, compared to other students.
                for (int i = 0; i < usrSpecificSets.Count; i++)
                {
                    usrSpecificSets[i].sameCount = usrSpecificSets
                        .Where(x => x.Percentage.Percentage == usrSpecificSets[i].Percentage.Percentage)
                        .Count();
                }

                // Get the percentage of questions by their induvidual types.
                List = (from question in allQuestions
                        group question by question.Type.TypeID into QGroup
                        select new PercentageModel
                        {
                            Current = QGroup.Where(x => x.AnsweredCorrect > 0).Count(),
                            Total = QGroup.Count()
                        }).ToArray();

                //  Compile the name of question types.
                questionTypes = (from question in allQuestions
                                 select question.Type.Type_Name).ToArray();
                // Remove all repeats from the array.
                questionTypes = questionTypes.Unique().ToArray();
            }

            // Set the class variable equal to the local list.
            Averages = usrSpecificSets.ToArray();
            Question = allQuestions.ToArray();

            // Calculate total percentage for the group.
            Percentage = new PercentageModel()
            {
                Current = correct,
                Total = total
            };

        }
        public PercentageModel Percentage { get; set; }
        public CompletedQuestion[] Question { get; set; }
        public UserQuestions[] Averages { get; set; }
        public PercentageModel[] List { get; set; }
        public string[] questionTypes { get; set; }
    }

    public class UserQuestions
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public int Attempts { get; set; }
        public PercentageModel Percentage { get; set; }
        public PercentageModel WorstPercentage { get; set; }
        public int NumberAnswered { get; set; }
        public int sameCount { get; set; }
        public CompletedQuestion[][] Questions { get; set; }
        public string bestType { get; set; }
        public string worstType { get; set; }
    }
}
