
function clickOnDelete(object) {

    $(object).parents().eq(3).find('div').eq(0).find('input').eq(1).val('true');
    $(object).parents().eq(6).find('form').eq(0).submit();

    return false;
}

function myAjaxRequest(myUrl, divTargetId, id) {

    $.ajax({
        type: "GET",
        url: myUrl + "?Id=" + id,
        contentType: "application/json; charset=utf-8",
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
            UpdateCVLayout(personneData, cvData);
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


$(document.body).on('click', '#SubmitAddOrEditCV', function () {

    var FormAction = $('#CVform1').find('input').eq(2).val();

    var data = JSON.stringify({
        __RequestVerificationToken: $('#CVform1').find('input').eq(0).val(),
        Id: $('#CVform1').find('input').eq(1).val(),
        FormAction,
        Titre: $('#CVform1').find('input').eq(3).val(),
        MontrerPhoto: $('#CVform1').find('input').eq(4).is(':checked'),
        formationsIds, competencesIds, experiencesIds, languesIds
    });

    $.ajax({

        contentType: 'application/json; charset=utf-8',
        type: 'POST',
        url: '/CV/AddOrEditCV',
        data: data,
        success: function (result) {
            $('#AffichageCVs').html(result);

            if (FormAction == "AjoutTraitement") {

                ResetCVForm1($('#CVform1'));
                $('#DivAlert').html("<b>Un CV vient dêtre ajouté.</b>");
                ClearCVLayout();

            } else {

                // on veut que la variable global json cvData soit mise à jour, du coup on refait un appel ajax en get
                ResetCVForm1($('#CVform1'));
                myAjaxRequestEditCV1('/CV/AddOrEditCV', '#AffichageCVForm', cvData.Id);
                $('#DivAlert').html("<b>Ce CV vient dêtre édité.</b>");
                UpdateCVLayout(personneData, cvData);
                //TODO : faire un fade out
                // mettre le $('#DivAlert').html("<b>Ce CV vient dêtre édité.</b>") dans le "success: function(result)" dans myAjaxRequestEditCV1()
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

$(document.body).on('click', '#CVform0 .fa-edit', function () {

    ResetCVForm1($('#CVform1'));

    myAjaxRequestEditCV1('/CV/AddOrEditCV', '#AffichageCVForm', $(this).attr('data-internalid'));
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


//__________________________________CV Layout_________________________________
//____________________________________________________________________________


function UpdateCVLayout(personne, cv) {

    //var data = {"Id":4,"Titre":"coucou","Layout":0,"MontrerPhoto":true,"Personne":null,"PersonneId":0,"Langues":[{"Id":2,"Label":"français","Niveau":3,"FormAction":"Ajout","FormTitre":"Nouvelle langue","CVs":null}],"Experiences":[{"Id":2,"DateDebut":"\/Date(1280700000000)\/","DateFin":"\/Date(1386889200000)\/","Entreprise":"Auto entrepreneur","Poste":"Webmestre","Description":null,"FormAction":"Ajout","FormTitre":"Nouvelle formation","CVs":null},{"Id":3,"DateDebut":"\/Date(1438466400000)\/","DateFin":"\/Date(1544655600000)\/","Entreprise":"Profroid","Poste":"cariste","Description":null,"FormAction":"Ajout","FormTitre":"Nouvelle formation","CVs":null}],"Formations":[{"Id":2,"DateDebut":"\/Date(1551481200000)\/","DateFin":"\/Date(1551740400000)\/","Ecole":"Leeds","Description":null,"Diplome":"Agrégation","FormAction":"Ajout","FormTitre":"Nouvelle formation","CVs":null},{"Id":3,"DateDebut":"\/Date(1551481200000)\/","DateFin":"\/Date(1551740400000)\/","Ecole":"Princeton","Description":null,"Diplome":"these","FormAction":"Ajout","FormTitre":"Nouvelle formation","CVs":null}],"Competences":[{"Id":2,"Label":"CSS3","Details":null,"FormAction":"Ajout","FormTitre":"Nouvelle formation","CVs":null},{"Id":3,"Label":"bootstrap","Details":null,"FormAction":"Ajout","FormTitre":"Nouvelle formation","CVs":null}],"FormationsIds":null,"CompetencesIds":null,"LanguesIds":null,"ExperiencesIds":null,"FormAction":"EditionTraitement","FormTitre":"Edition de ce CV"};

    if (cv.MontrerPhoto) {
        $('#CVPhotoCVLayout img').attr('src', '/Images/' + personne.Photo);
    } else {
        $('#CVPhotoCVLayout img').attr('src', '');
    }

    $('#CVTitreCVLayout').find('span').eq(0).html(personne.Prenom + " " + personne.Nom);
    $('#CVTitreCVLayout').find('span').eq(1).html(cv.Titre);

    $('#PersonneCVLayout').find('span').eq(1).html(personne.Email);
    $('#PersonneCVLayout').find('span').eq(3).html(personne.Adresse + "<br/>" + personne.CodePostal + " " + personne.Commune );
    $('#PersonneCVLayout').find('span').eq(5).html(personne.NumeroTel);

    if(personne.Permis){
        $('#PersonneCVLayout').find('span').eq(7).html("Permis de conduire");
    } else {
        $('#PersonneCVLayout').find('span').eq(7).html("Pas de permis de conduire");
    }

    //--------------------------------------experiences--------------------------------------------------

    var htmlTags1 = "<div class=\"row grid-striped\" style=\"font-size:0.5em\"><div class=\"col-3\">";
    var htmlTags2 = "</div><div class=\"col-9\">";
    var htmlTags3 = "</div></div>";
    var htmlExperiences = '';
    var dateDebutMois;
    var dateDebutAnnee;
    var dateFinMois;
    var dateFinAnnee;

    for (var i = 0; i < cv.Experiences.length; i++) {

        dateDebutMois = getMonthAndYear(cv.Experiences[i].DateDebut).dateMois;
        dateDebutAnnee = getMonthAndYear(cv.Experiences[i].DateDebut).dateAnnee;
        dateFinMois = getMonthAndYear(cv.Experiences[i].DateFin).dateMois;
        dateFinAnnee = getMonthAndYear(cv.Experiences[i].DateFin).dateAnnee;

        htmlExperiences += htmlTags1 + dateDebutMois + " / " + dateDebutAnnee + " - " + dateFinMois + " / " + dateFinAnnee + htmlTags2 + "Entreprise : " +
            cv.Experiences[i].Entreprise + "<br/> Poste : " + cv.Experiences[i].Poste + "<br/>";

        if (cv.Experiences[i].Description != null) {
            htmlExperiences += cv.Experiences[i].Description;
        }

        htmlExperiences += htmlTags3;
            
    }

    $('#CVDataCVLayout').find('div').eq(0).html(htmlExperiences);

    //--------------------------------------competences--------------------------------------------------

    htmlTags1 = "<span style=\"font-size:0.5em\">";
    htmlTags2 = "</span>";

    var htmlCompetences = htmlTags1;

    for (var i = 0; i < cv.Competences.length; i++) {
        //TODO : rajouter "Details"
        htmlCompetences += cv.Competences[i].Label + " - ";
    }

    htmlCompetences += htmlTags2;

    $('#CVDataCVLayout').find('div').eq(0).siblings('div').eq(0).html(htmlCompetences);

    //--------------------------------------formations--------------------------------------------------

    htmlTags1 = "<div class=\"row grid-striped\" style=\"font-size:0.5em\"><div class=\"col-3\">";
    htmlTags2 = "</div><div class=\"col-9\">";
    htmlTags3 = "</div></div>";
    var htmlFormations = '';
    var dateDebutMois;
    var dateDebutAnnee;
    var dateFinMois;
    var dateFinAnnee;

    for (var i = 0; i < cv.Formations.length; i++) {

        dateDebutMois = getMonthAndYear(cv.Formations[i].DateDebut).dateMois;
        dateDebutAnnee = getMonthAndYear(cv.Formations[i].DateDebut).dateAnnee;
        dateFinMois = getMonthAndYear(cv.Formations[i].DateFin).dateMois;
        dateFinAnnee = getMonthAndYear(cv.Formations[i].DateFin).dateAnnee;

        htmlFormations += htmlTags1 + dateDebutMois + " / " + dateDebutAnnee + " - " + dateFinMois + " / " + dateFinAnnee + htmlTags2 + "Ecole : " +
            cv.Formations[i].Ecole + "<br/> Diplome : " + cv.Formations[i].Diplome + "<br/>";

        if (cv.Formations[i].Description != null) {
            htmlFormations += cv.Formations[i].Description;
        }

        htmlFormations += htmlTags3;

    }

    //$('#CVDataCVLayout').find('div').eq(0).html(htmlFormations);
    $('#CVDataCVLayout').find('div').eq(0).siblings('div').eq(1).html(htmlFormations);

    //--------------------------------------langues--------------------------------------------------

    htmlTags1 = "<span style=\"font-size:0.5em\">";
    htmlTags2 = "</span>";

    var htmlLangues = "";

    for (var i = 0; i < cv.Langues.length; i++) {
        htmlLangues += htmlTags1 + cv.Langues[i].Label + " - Niveau : " + cv.Langues[i].Niveau + htmlTags2 + "<br/>" ;
    }

    $('#CVDataCVLayout').find('div').eq(0).siblings('div').eq(2).html(htmlLangues);

}



function ClearCVLayout() {

    $('#CVPhotoCVLayout img').attr('src', '');

    $('#CVTitreCVLayout').find('span').eq(0).html("");
    $('#CVTitreCVLayout').find('span').eq(1).html("");

    $('#PersonneCVLayout').find('span').eq(1).html("");
    $('#PersonneCVLayout').find('span').eq(3).html("");
    $('#PersonneCVLayout').find('span').eq(5).html("");

    $('#PersonneCVLayout').find('span').eq(7).html("");

    $('#CVDataCVLayout').find('div').eq(0).html("");
    $('#CVDataCVLayout').find('div').eq(0).siblings('div').eq(0).html("");
    $('#CVDataCVLayout').find('div').eq(0).siblings('div').eq(1).html("");
    $('#CVDataCVLayout').find('div').eq(0).siblings('div').eq(2).html("");

}


function getMonthAndYear(maDate) {
    
    var date = new Date(parseInt(maDate.substr(6)));
    var dateMois = parseInt(date.getMonth(), 10);
    dateMois += 1;
    var dateAnnee = parseInt(date.getYear(), 10) + 1900;

    var returnedJson = { dateMois: dateMois, dateAnnee: dateAnnee };

    return returnedJson;

    
}