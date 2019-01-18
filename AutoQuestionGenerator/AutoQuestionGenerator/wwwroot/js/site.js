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

function sortTable(n, id) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById(id);
    switching = true;

    var headers = table.rows[0].getElementsByTagName("TH");
    var del = document.getElementById("posSpan");
    if (del != null) {
        del.remove();
    }
    var span = document.createElement("i");
    span.id = "posSpan";
    span.classList = "up";
    headers[n].appendChild(span);

    dir = "asc";
    while (switching) {
        switching = false;
        rows = table.rows;

        for (i = 1; i < (rows.length - 1); i++) {

            shouldSwitch = false;

            x = rows[i].getElementsByTagName("TD")[n];
            y = rows[i + 1].getElementsByTagName("TD")[n];

            /*check if the two rows should switch place,*/

            if (dir == "asc") {
                if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {

                    //if so, mark as a switch.
                    shouldSwitch = true;
                    break;
                }
            } else if (dir == "desc") {
                if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {

                    //if so, mark as a switch
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            /*If a switch has been marked, make the switch*/
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            //Each time a switch is done, increase this count by 1:
            switchcount++;
        } else {
            /*If no switching has been done AND the direction is "asc"*/

            if (switchcount == 0 && dir == "asc") {
                span.classList = "down";
                dir = "desc";
                switching = true;
            }
        }
    }
}