$(".thousands").each(function () {
    checkComma($(this));
});
$(".thousands").on("keyup change", function (e) {
    checkComma($(this));
});


const modalArr = [...document.querySelectorAll('.modal')];
modalArr.forEach(m => {
    m.addEventListener('show.bs.modal', function (event) {
        $(".thousands").each(function () {
            checkComma($(this));
        });

        $(".thousands").on("keyup change", function (e) {
            checkComma($(this));
        });
        //const inputs = [...document.querySelectorAll('input[type=text].thousands')];
        //inputs.forEach(item => {
        //    item.value = '';
        //})
    });
});


function numberWithCommas(x) {
    var parts = x.toString().split('.');
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join('.');
}
String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, "g"), replacement);
};
function checkComma(element) {
    let number;
    if ($(element).is("input")) {
        number = $(element).val();
    } else {
        number = $(element).text();
    }
    if (number) {
        if (!isNaN(number) && isFinite(number)) {
            var num = numberWithCommas(number);
            if ($(element).is("input")) {
                $(element).val(num);
            } else {
                $(element).text(num);
            }
        }
    }
}

function Sub3Number(number) {
    if (number) {
        if (!isNaN(number) && isFinite(number)) {
            return numberWithCommas(number);
        }
    }
    return 0;
}