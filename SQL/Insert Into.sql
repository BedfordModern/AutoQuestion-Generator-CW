INSERT INTO subscriptions(Subsription_Length, Subscription_Name, Subscription_Description) VALUES(28, 'One Month', 'A one month roling subscription');
INSERT INTO organisations VALUES(0, 'modern', '5000:JHmZy8x4c2G3aIk+KbxYRB4aRI6LEUfwuSKMkt1Z7EDc/ap8ZlxRVA3X++zZLt5FaL2r/8/pHIKArRgcIugqpQ==:UcM+/7RdHHiTUu40lDB3HcejiCm0UMPlfxfWheK45Mjt3OH1nSm7/pNWFea7KFDLPFpIzQypocMqUn6vggv3jXX41KRGoHEtBQKeCgqPfZ0jSVL7lOglcyU8ajqh7L/sYoBofr1v+gbc0f0vjxTPiHJyIn5Nwz6Z78NNMZXCLQNZpSvKgs+8g+XysPAyXUldR3f//L9nHfXgxf2YfhkEQv+kwodBYyLTp03nq3doSK+WfWbl+BqA0tU6U2MqtvfV1FB8YMXADLLVtUkUbymxgexKX7MTFmmy/vc9MEeY5LfO9LyHumURGV0CQXyXv1V+iKvkv89mEWaJ1tDMc90TaFpVitncYToybHtXDXrEpB/3Q+m+fxfqHbubWg9nw5FLL3WjXVwhKA7HrIh8sQDZw1AeXY6BzkyHLKrGqrV2BtCClL12Xu0dVTLxo5rhPC1PIjx0dAWhhzr6rfg/+utoygyN6FWbQjKOkM0yNsZgEq/zvGIJFjnB8+4p+pi6LaZvUWJnC4qH0b/oDsI9OAklKgrAYIKCHmJgw5d2GlVMxPXCeoLK0Hv8u270fnDhaCetze/jmyCmJXCRcaJnTOA6FUg4ZKg2buo2qXo8QC61vJghQuHsM6OzHPRswM6rYPa+8gsnvAZpyQeZIs39zs1Y7LHnWPMyM9ZXiIC3PCrVp28=', 'Bedford Modern School',(SELECT SubscriptionID FROM Subscriptions WHERE Subscription_Name = 'One Month'), '2018-12-25');
INSERT INTO users VALUES(0, 'dalhome3', '5000:EJ/lVo9Tzu0i0IWwP3lpHfJObxVCcU3+b/VhKr+tTcRiBRY3RlwII6ngKv0FL/mLl92JOnHug7k53XmtJ72/fA==:0U6AIbEyWfATuDlK5egTlawIUhJJt8wpVN5xJ+5ZraeDj+xo1otNbokr4u1b4P01dPX3hQkdSWvZbXKS/SAZPPKyLEoi2o7lpzLzRRwFnFqe3POztPMzOb2tkCtjR/LsJSKUj8J9A2HQT7n/Z60Gr/bHmlMEnFvFdf8TMVyLtHOpiOnkB47Dh8M+oLdBvQX/j6pZ+Jmpo/Etl15wL9HDui+kZL5rdIbxUVsJltPE63zOqzv7hUpKW4SeCSRauEpIAs4fOJ7QpVGrWH79TuOc7sRkIVGCBXM7bvHt9aP2UoNohZ8mCfkHfHavdlxFTBIwIV/JkuHX3RTbvL9E8Ru3qxDoFu1ZdgmPWw8hF/u9Z5oPF+Upq3lJ5bZJ5egIgrqqSrLC9Lm07VRW5zMfXkgyFDlguGkGGdtCM5ysg4xXk6Udj3Fgg2shSe5ZZmbn8pxAmdP5wOg2v6Z7SaIkwreCVVdY5ZcrTFT5xWf9cpP3puL8MaOGRnYTFDGa+1N8CdW2dKZ9S/w5kmv5DAVzsx738gDETBjTqIuZGy0mbO/84eMCoW2LvLduts21xZQ9Xn+T2yy3OoANJvQfmY9jkBBZeMWkxxrPIATJkbJVW91KJZUKZqmW18F8ISw/e6IbwbyWTvNWUFZpbrzaeVyFuAVBcPUt3F7zMlioUYShOoYPFX8=', 'Daniel', 'Ledger', (SELECT organisations.OrganisationID FROM organisations WHERE organisations.Organisation_Username='modern')
, '2018-10-16 13:34:59');

INSERT INTO accesstypes VALUES(1, 'All Teachers', 'Avaliable to all teachers in the organisation.');
INSERT INTO group_table VALUES(1, '13B-CS', 02, 1);

INSERT INTO settypes VALUES(1, 'Per Question Answering');

INSERT INTO questiontypes VALUES(0, 'Binary Integers', 'testPart.py');
INSERT INTO worksets VALUES(0, 1, 2, 1,0, NOW(), NOW());
INSERT INTO work VALUES(0, 3, 192, 1);
INSERT INTO work VALUES(0, 3, 197, 1);
INSERT INTO work VALUES(0, 3, 255, 1);