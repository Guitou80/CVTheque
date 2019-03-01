
function clickOnDelete(object) {

    $(object).parents().eq(3).find('div').eq(0).find('input').eq(1).val('true');
    $(object).parents().eq(6).find('form').eq(0).submit();

    debugger;

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

//---------------------------------------------------------------------------------------------------------------
//---------------------------------------------Image crop dans Enregistrement------------------------------------



