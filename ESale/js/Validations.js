
function alphanumericincludeingunderscore(textbox) {
    var alphane = textbox.value
    var numaric = alphane;
    for (var j = 0; j < numaric.length; j++) {

        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if ((hh > 47 && hh < 58) || (hh > 64 && hh < 91) || (hh > 94 && hh < 123) || (hh == 46) || (hh == 32)) {
        }
        else {
            alert("Please enter alphanumerics only...");
            textbox.value = ''
            textbox.focus();
            return false;
        }
    }

    return true;
}


function alphanumeric(textbox) {
    var alphane = textbox.value
    var numaric = alphane;
    for (var j = 0; j < numaric.length; j++) {

        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if ((hh > 47 && hh < 58) || (hh > 64 && hh < 91) || (hh > 94 && hh < 123) || (hh == 32)) {
        }
        else {
            alert("Please enter alphanumerics only...");
            textbox.value = ''
            textbox.focus();
            return false;
        }
    }

    return true;
}




function alphanumericincludingsapcanddot(textbox) {
    var alphane = textbox.value
    var numaric = alphane;
    for (var j = 0; j < numaric.length; j++) {

        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if ((hh > 47 && hh < 58) || (hh > 64 && hh < 91) || (hh > 94 && hh < 123) || (hh == 32) || (hh == 46)) {
        }
        else {
            alert("Please enter alphanumerics only...");
            textbox.value = ''
            textbox.focus();
            return false;
        }
    }

    return true;
}

function Onlynumbersandminus(textbox) {
    var alphane = textbox.value
    var numaric = alphane;
    for (var j = 0; j < numaric.length; j++) {

        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if ((hh > 47 && hh < 58) || (hh > 95 && hh < 106) || (hh == 45)) {
        }
        else {
            alert("Please enter Numbers and '-' only...");
            textbox.value = ''
            textbox.focus();
            return false;
        }
    }

    return true;
}



function alphanumericincludingsapcanddotandcomma(textbox) {
    var alphane = textbox.value
    var numaric = alphane;
    for (var j = 0; j < numaric.length; j++) {

        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if ((hh > 47 && hh < 58) || (hh > 64 && hh < 91) || (hh > 94 && hh < 123) || (hh == 32) || (hh == 46) || (hh == 44)) {
        }
        else {
            alert("Please enter alphanumerics only...");
            textbox.value = ''
            textbox.focus();
            return false;
        }
    }

    return true;
}


function restrictsingleanddoublequotes(textbox) {


    var alphane = textbox.value
    var numaric = alphane;
    for (var j = 0; j < numaric.length; j++) {

        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if (hh != 34 && hh != 39)
        { }

        else {
            alert("Please don't enter single and doublecodes alphabets only...");
            textbox.value = ''
            textbox.focus();
            return false;
        }
    }

    return true;
}

function onlyalphabets(textbox) {
    var alphane = textbox.value
    var numaric = alphane;
    for (var j = 0; j < numaric.length; j++) {
        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if (hh != 34 && hh != 39)
        { }
        if ((hh > 64 && hh < 91) || (hh > 96 && hh < 123) || (hh == 46) || (hh == 8) || (hh == 32)) {
        }
        else {
            alert("Please enter alphabets only...");
            textbox.value = ''
            textbox.focus();
            return false;
            
        }
    }
    return true;
}




function onlyalphabetsandspace(textbox) {
    var alphane = textbox.value
    var numaric = alphane;
    for (var j = 0; j < numaric.length; j++) {
        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if (hh != 34 && hh != 39)
        { }
        if ((hh > 64 && hh < 91) || (hh > 96 && hh < 123) || (hh == 46) || (hh == 8) || (hh == 32)) {
        }
        else {
            alert("Please enter alphabets only...");
            textbox.value = ''
            textbox.focus();
            return false;
        }
    }
    return true;
}





function onlyCharacter(KeyCode) {
    if ((event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 97 && event.keyCode <= 122) || (event.keyCode == 32)) {
        event.returnValue = true;

    }
    else {
        alert("Please Enter Characters Only...");
        event.keyCode.value = "";
        textbox.focus();
        event.returnValue = false;
       
    }
}


function alphabetswithdotsinglequoteandspace(textbox) {
    var alphane = textbox.value
    var numaric = alphane;
    for (var j = 0; j < numaric.length; j++) {
        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if (hh != 34 && hh != 39)
        { }
        if ((hh > 64 && hh < 91) || (hh > 96 && hh < 123) || (hh == 46) || (hh == 8) || (hh == 39) || (hh == 32) || (hh == 46)) {
        }
        else {
            alert("Please enter alphabets only...");
            textbox.value = ''
            return false;
            textbox.focus();
        }
    }
    return true;
}

function onlyIntegers(textbox) {
    var alphane = textbox.value
    var numaric = alphane;

    for (var j = 0; j < numaric.length; j++) {
        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if (hh != 34 && hh != 39)
        { }
        if ((hh > 47 && hh < 58)) {
        }
        else {
            alert("Please enter numbers only...");
            textbox.value = ''
            textbox.focus();
            return false;
        }
    }
    return true;
}




function onlyIntegersincludingdot(textbox) {
    var alphane = textbox.value
    var numaric = alphane;

    for (var j = 0; j < numaric.length; j++) {
        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if (hh != 34 && hh != 39)
        { }
        if ((hh > 45 && hh < 58)) {
        }
        else {
            alert("Please enter numbers only...");
            textbox.value = ''
            textbox.focus();
            return false;
        }
    }
    return true;
}

function onlyalphabetsandcomma(textbox) {
    var alphane = textbox.value
    var numaric = alphane;
    for (var j = 0; j < numaric.length; j++) {
        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if (hh != 34 && hh != 39)
        { }
        if ((hh > 64 && hh < 91) || (hh > 96 && hh < 123) || (hh == 46) || (hh == 8) || (hh == 44)) {
        }
        else {
            alert("Please enter alphabets and comma only...");
            textbox.value = ''
            textbox.focus();
            return false;
        }
    }
    return true;
}

function onlyalphabetsandDots(textbox) {
    var alphane = textbox.value
    var numaric = alphane;
    for (var j = 0; j < numaric.length; j++) {
        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if (hh != 34 && hh != 39)
        { }
        if ((hh > 64 && hh < 91) || (hh > 96 && hh < 123) || (hh == 46) || (hh == 32)) {
        }
        else {
            alert("Please enter alphabets only...");
            textbox.value = ''
            textbox.focus();
            return false;
        }
    }
    return true;
}


function changeIt() {
    var theImg = document.getElementsByTagName('img')[0].src

    var x = theImg.split("/");
    var t = x.length - 1;
    var y = x[t];

    if (y == 'c3.jpg') {
        document.images.example.src = "../ESMImages/c4.jpg";
        parent.resizeFrames(3);

    }
    if (y == 'c4.jpg') {
        document.images.example.src = "../ESMImages/c3.jpg";
        parent.resizeFrames(4);
    }

}

function alphanumericincludeingunderscorewithoutSpace(textbox) {
    var alphane = textbox.value
    var numaric = alphane;
    for (var j = 0; j < numaric.length; j++) {

        var alphaa = numaric.charAt(j);
        var hh = alphaa.charCodeAt(0);
        if ((hh > 47 && hh < 58) || (hh > 64 && hh < 91) || (hh > 94 && hh < 123) || (hh == 46)) {
        }
        else {
            alert("Please enter alphanumerics only...");
            textbox.value = ''
            textbox.focus();
            return false;
        }
    }

    return true;
}


function validTwoDecimal(textBox) {
//    var tenthPer = document.getElementById("ctl00_ContentPlaceHolder1_txt10thPer").value;
//    var interPer = document.getElementById("ctl00_ContentPlaceHolder1_txt12thPer").value;
//    var gradPer = document.getElementById("ctl00_ContentPlaceHolder1_txtGradPer").value;
    //    var postGradPer = document.getElementById();


    var per = textBox.value;
    if (per > 100) {
        alert("Enter percentage less than or equal to 100");
        textBox.value = "";
        textBox.focus();
        return false;
    }
    return true;

}

function ValidateDate() {
    var day1, day2, day3, day4;
    var month1, month2, month3, month4;
    var year1, year2, year3, year4;
    var D = new Date();

    var value1 = document.getElementById('ctl00_ContentPlaceHolder1_txtDob').value;
    

    day1 = value1.substring(0, value1.indexOf("-"));
    month1 = value1.substring(value1.indexOf("-") + 1, value1.lastIndexOf("-"));
    year1 = value1.substring(value1.lastIndexOf("-") + 1, value1.length);

//    day2 = value2.substring(0, value2.indexOf("-"));
//    month2 = value2.substring(value2.indexOf("-") + 1, value2.lastIndexOf("-"));
    //    year2 = value2.substring(value2.lastIndexOf("-") + 1, value2.length);

    day2 = D.getDate();
    month2 = D.getMonth()+1;// Because it starts indexing from january as 0 and february as 1
    year2 = D.getFullYear();

    var date1 = year1 + "/" + month1 + "/" + day1;
   
    var date2 = year2 + "/" + month2 + "/" + day2;


    firstDate = Date.parse(date1);
   
    secondDate = Date.parse(date2);



    msPerDay = 24 * 60 * 60 * 1000
//    var performance = Math.round((secondDate.valueOf() - firstDate.valueOf()) / msPerDay) + 1;
    var performance = Math.round((firstDate.valueOf()-secondDate.valueOf()) / msPerDay)+1;

    if (performance>0) {
        alert('Date sholud not be greater than or equal to current Date');
        document.getElementById('ctl00_ContentPlaceHolder1_txtDob').value = '';
    }


}

