// Write your JavaScript code.

function NextClick() {
    if (document.getElementById("nextButton").innerHTML == "Next") {
        document.getElementById("CreatetionForm").submit();
    } else {
        var win = window.open("/groups/create?closeAfter=true", "MsgWindow", "width=1000,height=800");
        win.document.addEventListener("unload", function () {
            if (win.location.href.includes("/groups/create")) {

            }
        });
        win.addEventListener('load', function () {
            var button = win.document.getElementById("createGroupButton");
            button.onclick = function () {
                var dta = win.$("form").serialize();
                win.$.ajax({
                    type: 'POST',
                    url: win.$("createGroupForm").attr("action"),
                    data: dta,

                    success: function (result) {
                        var x = document.getElementById("groupSelector");
                        var res = result.split(";")
                        if (x.selectedIndex >= 0) {
                            var option = document.createElement("option");
                            option.value = res[0];
                            option.text = res[1];
                            x.add(option);
                        }
                        win.close();
                    },
                });
            }
        }, false);
    }
}

function GroupSelectorOnChange() {
    var e = document.getElementById("groupSelector");
    var strOut = e.options[e.selectedIndex].value;
    if (strOut == "-1") {
        document.getElementById("nextButton").innerHTML = "Create"
    } else {
        document.getElementById("nextButton").innerHTML = "Next"
    }
}

$(document).ready(GroupSelectorOnChange)
