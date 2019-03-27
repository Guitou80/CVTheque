
function clickOnDelete(object) {

    $(object).parents().eq(3).find('div').eq(0).find('input').eq(1).val('true');
    $(object).parents().eq(6).find('form').eq(0).submit();

    return false;
}

function myAjaxRequest(myUrl, divTargetId, id) {

    $.ajax({
        type: "GET",
        url: myUrl + "?Id=" + id,
        //data: myData,
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: function (response) {
            $(divTargetId).html(response);
        },
        failure: function (response) {
            alert("failure");
            alert(response.responseText);
        },
        error: function (response) {
            alert("error");
            alert(response.responseText);
        }
    });

    return false;
}

//-------------------------------------------------------------------------
//----------------------Formations-----------------------------------------

$(document.body).on('click', '#form0 .fa-edit', function () {
    myAjaxRequest('/Donnees/AddOrEditFormation', '#AffichageFormationForm', $(this).attr('data-internalid')); });

$(document.body).on('click', '#form0 .fa-trash-alt', function () { clickOnDelete(this); });


$(document.body).on('click', '#form0 .fa-plus-square', function () {

    ResetForm1($('#form1'));

    return false;

});

function ResetForm1(form) {

    $(form).attr('data-ajax-update', '#AffichageFormations');
    //$(form).attr('method', 'post');
    $(form).find('span.action').text('Nouvelle formation');
    $(form).find('input[name=FormAction]').attr('value', 'AjoutTraitement');
    $(form).find('input.form-control:eq(2)').val('');
    $(form).find('input.form-control:eq(3)').val('');
    $(form).find('textarea').val('');

}

//-------------------------------------------------------------------------
//----------------------Competences-----------------------------------------

$(document.body).on('click', '#form2 .fa-edit', function () {
    myAjaxRequest('/Donnees/AddOrEditCompetence', '#AffichageCompetenceForm', $(this).attr('data-internalid'));
});

$(document.body).on('click', '#form2 .fa-trash-alt', function () { clickOnDelete(this); });


$(document.body).on('click', '#form2 .fa-plus-square', function () {

    ResetForm3($('#form3'));

    return false;

});

function ResetForm3(form) {

    $(form).attr('data-ajax-update', '#AffichageCompetences');
    $(form).find('span.action').text('Nouvelle competence');
    $(form).find('input[name=FormAction]').attr('value', 'AjoutTraitement');
    $(form).find('input.form-control:eq(0)').val('');
    $(form).find('textarea').val('');

}

//-------------------------------------------------------------------------
//----------------------Experiences-----------------------------------------

$(document.body).on('click', '#form4 .fa-edit', function () {
    myAjaxRequest('/Donnees/AddOrEditExperience', '#AffichageExperienceForm', $(this).attr('data-internalid'));
});

$(document.body).on('click', '#form4 .fa-trash-alt', function () { clickOnDelete(this); });


$(document.body).on('click', '#form4 .fa-plus-square', function () {

    ResetForm5($('#form5'));

    return false;

});

function ResetForm5(form) {

    $(form).attr('data-ajax-update', '#AffichageExperiences');
    $(form).find('span.action').text('Nouvelle experience');
    $(form).find('input[name=FormAction]').attr('value', 'AjoutTraitement');
    $(form).find('input.form-control:eq(2)').val('');
    $(form).find('input.form-control:eq(3)').val('');
    $(form).find('textarea').val('');

}

//-------------------------------------------------------------------------
//--------------------------------Langues---------------------------------

$(document.body).on('click', '#form6 .fa-edit', function () {
    myAjaxRequest('/Donnees/AddOrEditLangue', '#AffichageLangueForm', $(this).attr('data-internalid'));
});

$(document.body).on('click', '#form6 .fa-trash-alt', function () { clickOnDelete(this); });


$(document.body).on('click', '#form6 .fa-plus-square', function () {

    ResetForm7($('#form7'));

    return false;

});

function ResetForm7(form) {

    $(form).attr('data-ajax-update', '#AffichageLangues');
    $(form).find('span.action').text('Nouvelle Langue');
    $(form).find('input[name=FormAction]').attr('value', 'AjoutTraitement');
    $(form).find('input.form-control:eq(0)').val('');

}

//-------------------------------------------------------------------------
//-------------------------------------------------------------------------
//--------------------------------CVs----------------------------------
//-------------------------------------------------------------------------

var formationsIds = [];
var competencesIds = [];
var experiencesIds = [];
var languesIds = [];

function clearAllDataHighlights() {

    $('div.highlightable').each(function () {

        var btnElement = $(this).find('div.col-1 > span > i');
        var divId = $(this).attr('data-id');
        var dataName = $(this).parents().eq(0).attr('id');

        $(this).css("background-color", "");
        $(btnElement).removeClass('fa-minus-square').addClass('fa-plus-square');
        $(btnElement).parents().eq(0).removeClass('black').addClass('green');

        
        formationsIds = [];
        competencesIds = [];
        experiencesIds = [];
        languesIds = [];
        
    });

}


function SwitchDataHighlight(div, updateDataIdsArrays) {

    var btnElement = $(div).find('div.col-1 > span > i');
    var divId = $(div).attr('data-id');
    var dataName = $(div).parents().eq(0).attr('id');

    if ($(btnElement).attr("class") == "fas fa-plus-square") {

        $(div).css("background-color", "green");
        $(btnElement).removeClass('fa-plus-square').addClass('fa-minus-square');
        $(btnElement).parents().eq(0).removeClass('green').addClass('black');

        if (updateDataIdsArrays) {
            switch (dataName) {
                case 'formations':
                    formationsIds.push(divId);
                    break;
                case 'competences':
                    competencesIds.push(divId);
                    break;
                case 'experiences':
                    experiencesIds.push(divId);
                    break;
                case 'langues':
                    languesIds.push(divId);
                    break;
            }
        }
        

    } else {

        $(div).css("background-color", "");
        $(btnElement).removeClass('fa-minus-square').addClass('fa-plus-square');
        $(btnElement).parents().eq(0).removeClass('black').addClass('green');

        if (updateDataIdsArrays) {
            switch (dataName) {
                case 'formations':
                    var index = formationsIds.indexOf(divId);
                    if (index > -1) {
                        formationsIds.splice(index, 1);
                    }
                    
                    break;
                case 'competences':
                    var index = competencesIds.indexOf(divId);
                    if (index > -1) {
                        competencesIds.splice(index, 1);
                    }
                    
                    break;
                case 'experiences':
                    var index = experiencesIds.indexOf(divId);
                    if (index > -1) {
                        experiencesIds.splice(index, 1);
                    }
                    
                    break;
                case 'langues':
                    var index = languesIds.indexOf(divId);
                    if (index > -1) {
                        languesIds.splice(index, 1);
                    }
                    
                    break;
            }
        }

    }

}

// met en surbrillance verte les données présentes dans le CV initialement
function highlightInitialData() {

    if (formationsIds.length > 0) {

        $('#formations > div').each(function () {

            if ($(this).attr("data-id") != undefined) {
                if (formationsIds.includes($(this).attr("data-id"))) {
                    SwitchDataHighlight($(this), false);
                }
            }

        });

    }

    if (competencesIds.length > 0) {

        $('#competences > div').each(function () {

            if ($(this).attr("data-id") != undefined) {

                if (competencesIds.includes($(this).attr("data-id"))) {
                    SwitchDataHighlight($(this), false);
                }
            }

        });
        
    }

    if (experiencesIds.length > 0) {

        $('#experiences > div').each(function () {

            if ($(this).attr("data-id") != undefined) {

                if (experiencesIds.includes($(this).attr("data-id"))) {
                    SwitchDataHighlight($(this), false);
                }
            }

        });

    }


    if (languesIds.length > 0) {

        $('#langues > div').each(function () {

            if ($(this).attr("data-id") != undefined) {

                if (languesIds.includes($(this).attr("data-id"))) {
                    SwitchDataHighlight($(this), false);
                }
            }

        });

    }

}

//stocke les Id respectifs de toute les données présentes dans le CV
function setDonneesIdsArrays(divId) {
    
    $(divId).find('input[type = hidden]').each(function () {
        if ($(this).attr("name") == "FormationsIds[]") {
            formationsIds.push(($(this).attr("value")));
        }
        if ($(this).attr("name") == "CompetencesIds[]") {
            competencesIds.push(($(this).attr("value")));
        }
        if ($(this).attr("name") == "ExperiencesIds[]") {
            experiencesIds.push(($(this).attr("value")));
        }
        if ($(this).attr("name") == "LanguesIds[]") {
            languesIds.push(($(this).attr("value")));
        }
    });

}



function myAjaxRequestEditCV1(myUrl, divTargetId, id) {

    $.ajax({
        type: "GET",
        url: myUrl + "?Id=" + id,
        //data: myData,
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: function (response) {
            $(divTargetId).html(response);
            setDonneesIdsArrays(divTargetId);
            highlightInitialData();
        }
       ,
        failure: function (response) {
            alert("failure");
            alert(response.responseText);
        },
        error: function (response) {
            alert("error");
            alert(response.responseText);
        }
    });

    return false;
}



$(document.body).on('click', '#CVform0 .fa-edit', function () {

    ResetCVForm1($('#CVform1'));

    myAjaxRequestEditCV1('/CV/AddOrEditCV', '#AffichageCVForm', $(this).attr('data-internalid'));
});


$(document.body).on('click', '#SubmitAddOrEditCV', function () {

    var FormAction = $('#CVform1').find('input').eq(2).val();

    var data = JSON.stringify({
        __RequestVerificationToken: $('#CVform1').find('input').eq(0).val(),
        Id: $('#CVform1').find('input').eq(1).val(),
        FormAction,
        Titre: $('#CVform1').find('input').eq(3).val(),
        MontrerPhoto: $('#CVform1').find('input').eq(4).val(),
        formationsIds, competencesIds, experiencesIds, languesIds
    });


    $.ajax({

        contentType: 'application/json; charset=utf-8',
        //dataType: 'json',
        type: 'POST',
        url: '/CV/AddOrEditCV',
        data: data,
        success: function (result) {
            //alert('success');
            $('#AffichageCVs').html(result);

            if (FormAction == "AjoutTraitement") {
                ResetCVForm1($('#CVform1'));
                $('#DivAlert').html("<b>Un CV vient dêtre ajouté.</b>");
            } else {
                $('#DivAlert').html("<b>Ce CV vient dêtre édité.</b>");
                //TODO : faire un fade out
            }

            $('#DivAlert').addClass('alert alert-success');

        },
        failure: function (result) {
            alert("failure");
        },
        error: function (result) {
            alert("error");
        }
    });

    //debugger

});


//édition d'un cv
$(document.body).on('click', '#CVform0 .fa-trash-alt', function () {

    $(this).parents().eq(3).find('div').eq(0).find('input').eq(1).val('true');
    $(this).parents().eq(6).find('form').eq(0).submit();

    ResetCVForm1($('#CVform1'));

});

// ajout d'un nouveau cv
$(document.body).on('click', '#CVform0 .fa-plus-square', function () {

    ResetCVForm1($('#CVform1'));

    return false;

});

function ResetCVForm1(form) {

    $(form).attr('data-ajax-update', '#AffichageCVs');
    $(form).find('span.action').text('Nouveau CV');
    $(form).find('input[name=FormAction]').attr('value', 'AjoutTraitement');
    $(form).find('input.form-control:eq(0)').val('');
    $('#DivAlert').removeClass('alert alert-success');
    $('#DivAlert').html('');


    clearAllDataHighlights();

}

//_____________________ données dans CVs _______________________________________
//______________________________________________________________________________

// on clique dessus quand on veut mettre en surbrillance ou non l'élément
$(document.body).on('click', '#CVform1 .fas', function () {

    var mydiv = $(this).parents().eq(2);
    SwitchDataHighlight(mydiv, true);

});