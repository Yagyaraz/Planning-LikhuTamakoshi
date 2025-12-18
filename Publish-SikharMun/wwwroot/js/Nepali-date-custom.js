////// Write by (Alok) JavaScript code .


//#region Nepali date picker 
function GetNepaliCurrentDate() {
    var selectedDate = NepaliFunctions.GetCurrentBsDate();
    var month = (selectedDate.month.toString().length === 2) ? selectedDate.month : '0' + selectedDate.month;
    var day = (selectedDate.day.toString().length === 2) ? selectedDate.day : '0' + selectedDate.day;
    var date = selectedDate.year + '-' + month + '-' + day;
    return date;
}

function GetADDate() {   
    var selectedDate = NepaliFunctions.GetCurrentAdDate();
    var month = (selectedDate.month.toString().length == 2) ? selectedDate.month : '0' + selectedDate.month;
    var day = (selectedDate.day.toString().length == 2) ? selectedDate.day : '0' + selectedDate.day;
    var date = selectedDate.year + '-' + month + '-' + day;
    return date;
}

function GetNepaliDateFromEnglishDate(adYear = 1, adMonth, adDay) {
    if (parseInt(adYear) > 1800) {
	    var selectedDate = NepaliFunctions.AD2BS({ year: adYear, month: adMonth, day: adDay });
	    var month = (selectedDate.month.toString().length == 2) ? selectedDate.month : '0' + selectedDate.month;
	    var day = (selectedDate.day.toString().length == 2) ? selectedDate.day : '0' + selectedDate.day;
	    var date = selectedDate.year + '-' + month + '-' + day;
	    return date;
    } else
	    return GetNepaliCurrentDate();
}



function GetAdDateFromNepaliDate(nepaliDate) {
    if (nepaliDate.trim()) {
        var aa = nepaliDate.split('-');
        var selectedDate = NepaliFunctions.BS2AD({ year: aa[0], month: aa[1], day: aa[2] });
        var adDate = new Date(`${selectedDate.year}-${selectedDate.month}-${selectedDate.day}`);
        return adDate.toLocaleDateString('en-CA');
    }
}

function GetAgeFromNepaliDate(nepaliDate) {
    if (nepaliDate.trim()) {
        var aa = nepaliDate.split('-');
        var selectedDate = NepaliFunctions.BS2AD({ year: aa[0], month: aa[1], day: aa[2] });
        var now = NepaliFunctions.GetCurrentAdDate();
        var age = now.year - selectedDate.year;
        return age;
    }
}

function NepaliDatePickerCal(date) {
    var cDate = GetNepaliCurrentDate();   
    date.nepaliDatePicker({
        ndpYear: true,
        ndpMonth: true,
        readOnlyInput: true,
        //disableAfter: cDate,
        onChange: function (a) {           
            date.value = a.bs;
        }
    });
    date.value = date.value || cDate;
}

function NepaliDatePickerCalWithClass(date) {
    var cDate = GetNepaliCurrentDate();
    date.nepaliDatePicker({
        ndpYear: true,
        ndpMonth: true,
        readOnlyInput: true,
        //disableAfter: cDate,
    });
    date[0].value = date[0].value || cDate;
}
function NepaliDatePickerCalForPartialViewWithADDate(date) {
    date.nepaliDatePicker({
        ndpYear: true,
        ndpMonth: true,
        readOnlyInput: true,
        onChange: function (a) {
            for (var i = 0; i < date.length; i++) {
                if (date[i].value === a.bs) {
                    SetDateValue(date, i, a.ad);
                }
            }
        }
    });
}

function SetDateValue(date, i, adDate) {
    date[i].closest('td').getElementsByClassName('AdDate')[0].value = adDate;
}

function NepaliDatePickerCalWithAdDateConverter(NpDate, AdDate) {    
	var cDate = GetNepaliCurrentDate();
    NpDate.nepaliDatePicker({
        ndpYear: true,
        ndpMonth: true,
        readOnlyInput: true,
      //  disableAfter: cDate,
        onChange: function (a) {
            AdDate.value = a.ad;
        }
    });
    NpDate.value = NpDate.value || cDate;
    AdDate.value = AdDate.value || GetADDate();
}


function NepaliDatePickerCalWithAdDateWithDisableNextDate(NpDate, AdDate) {   
    var currentDate = GetNepaliCurrentDate();
    NpDate.nepaliDatePicker({
        ndpYear: true,
        ndpMonth: true,
        readOnlyInput: true,
       // disableAfter: currentDate,
        onChange: function (a) {
            if (AdDate !== '') {
                AdDate.value = a.ad;
            }
        }
    });
  //  NpDate.value = currentDate;
    //if (AdDate !== '') {
    //    var dt = new Date(AdDate.value);
    //    var year = dt.getFullYear();
    //    if (year > 2001) {
    //        var month = dt.getMonth() + 1;
    //        var day = dt.getDate();
    //        NpDate.value = GetNepaliDateFromEnglishDate(year, month, day);
    //    } else {
	   //     AdDate.value = GetADDate();
    //    }
    //}
}
function AgeConverterInPartialview(dobBS) {    
    dobBS.nepaliDatePicker({        
        ndpYear: true,
        ndpMonth: true,
        readOnlyInput: true,
        disableAfter: GetNepaliCurrentDate(),
        onChange: function (a) {
            
            for (var i = 0; i < dobBS.length; i++) {
                if (dobBS[i].value === a.bs) {
                    dobBS[i].closest('.new').getElementsByClassName('pAge')[0].value = GetAgeFromNepaliDate(a.bs);
                }
            }
        }
    });
}
function NepaliDatePickerCalWithAdDateWithDisableNextDateForModal(NpDate, AdDate) {
    var currentDate = GetNepaliCurrentDate();
    NpDate.nepaliDatePicker({
        ndpYear: true,
        ndpMonth: true,
        readOnlyInput: true,
        autoclose: true,
        todayHighlight: true,
        container: '#largeModal',
        // disableAfter: currentDate,
        onChange: function (a) {
            if (AdDate !== '') {
                AdDate.value = a.ad;
            }
        }
    });
}
function NepaliDatePickerCalWithAdDateWithDisableNextDateForModal1(NpDate, AdDate) {    
    var currentDate = GetNepaliCurrentDate();
    NpDate.nepaliDatePicker({
        ndpYear: true,
        ndpMonth: true,
        readOnlyInput: true,
        autoclose: true,
        todayHighlight: true,
        container: '#largeModal1',
        // disableAfter: currentDate,
        onChange: function (a) {
            if (AdDate !== '') {
                AdDate.value = a.ad;
            }
        }
    });
}
function NepaliDatePickerStaticDateForClass(date) {
    var currentDate = GetNepaliCurrentDate();
    var nepaliDate = englishToNepali(currentDate);
    for (var i = 0; i < date.length; i++) {
        date[i].textContent = nepaliDate;
    }
}

function englishToNepali(engValue) {
    const textNepali = ["०", "१", "२", "३", "४", "५", "६", "७", "८", "९"];
    const textEnglish = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
    const inputText = engValue.split('');
    let nepValue = "";
    for (let j = 0; j < engValue.length; j++) {
        let v = inputText[j];
        let index = textEnglish.indexOf(v);
        nepValue += (index >= 0) ? textNepali[index] : v;
    }
    return nepValue;
}


function AgeConverterInPartialview(dobBS) {
    dobBS.nepaliDatePicker({
        ndpYear: true,
        ndpMonth: true,
        readOnlyInput: true,
        disableAfter: GetNepaliCurrentDate(),
        onChange: function (a) {

            for (var i = 0; i < dobBS.length; i++) {
                if (dobBS[i].value === a.bs) {
                    dobBS[i].closest('.new').getElementsByClassName('pAge')[0].value = GetAgeFromNepaliDate(a.bs);
                }
            }
        }
    });
}

$('.pDate').html(englishToNepali(GetNepaliCurrentDate()))
