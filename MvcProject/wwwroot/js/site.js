// #id - id selector
// .class - class selector
// tag - tag selector
// * - all selector
// [attr] - attribute selector
// h1 to console
var h1 = $("h1");
console.log(h1);
console.log($(".display-4").text());
// #text1 to console
var someText = $("#text1.t2").text();
console.log(someText);
// table2 td onclick
// table2 -> all td-s
$("#table2 td")
    .click(function (sender) {
    alert($(sender.target).text());
});

// table1 show/hide tbody by thead onclick
$("#table1 thead").click(function () {
    $("#table1 tbody").toggle();
});

// table2 row hover
$("#table2 tr").hover(function () {
    $(this).toggleClass("table-primary");
});

// fetch data from https://meowfacts.herokuapp.com/
// and show in #meowfact
$("#btnMeow").click(function () {
    $.get("https://meowfacts.herokuapp.com/", function (data) {
        $("#meowfact").text($("#meowfact").text() + data.data);
    });
});
