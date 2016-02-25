function loadPoll()
{
    $(".slide-next").on("click", function () {
        $('#frmPoll').validator('validate');
        var hasErrors = false;
        $('.with-errors').each(function () {
            if (!$(this).is(':empty')) {
                hasErrors = true;
            }
        });
        if (!hasErrors) {
            change($(this).data("slide") - 1, $(this).data("slide"), $(this).data("perc"));
        }
    });
    $(".slide-prev").on("click", function () {
        var hasErrors = false;
        $('.with-errors').each(function () {
            if (!$(this).is(':empty')) {
                hasErrors = true;
            }
        });
        if (!hasErrors) {
            change($(this).data("slide") + 1, $(this).data("slide"), $(this).data("perc"));
        }
    });
    $("#slide1").hide();
    $("#slide1").fadeIn(400);


    $('.zso_slider').each(function () {
        $(this).slider(
            {
                tooltip: 'always',
                min: $(this).attr("data-min-value"),
                max: $(this).attr("data-max-value"),
                step: $(this).attr("data-step-value"),
                slide: function (event, ui) {
                    $("#slidervalue").val(ui.value);
                    $(this).attr("title", ui.value);
                }
            }
        );
        });
}
function createJson() {
    var answers = new Array();
    $('.question').each(function () {
        var question = new Object();
        question.FrageID = $(this).attr("data-question-id");
        question.FrageTyp = $(this).attr("data-question-type");
        // Multiline Answer
        if (question.FrageTyp == "707b446e-4c25-4494-a18a-f444480c6e25") {
            question.answer = $(this).find("textarea").val();
            // Skala
        } else if (question.FrageTyp == "9c4c7610-fffc-4159-af45-0880f010936c") {
            question.answer = $(this).find("#slider").val();
        }   // Single Choice Radiobutton
        else if (question.FrageTyp == "d1609e72-54a2-4680-bc8b-02bd0ebaecf3") {
            //if ($(this, 'input[name=radio]:checked').attr("data-fragen-toggle") == "True") {
            if ($('input[name="' + question.FrageID + '"]:checked', '#frmPoll').attr("data-fragen-toggle") == "True") {
                question.answer = $("#" + $(this).find("input[type='radio']:checked").attr("data-antwort-id")).val();
            }
            else {
                question.answer = $('input[name="' + question.FrageID + '"]:checked', '#frmPoll').attr("data-antwort-id");
            }
        }  // Multiple Choice Checkbox
        else if (question.FrageTyp == "e0285169-1340-4b69-8411-e7272017a28a") {
            var checkboxAnswers = new Array();
            $(this).find("input[type='checkbox']:checked").each(function () {
                if ($(this).attr("id") == "zusatzCheckbox") {
                    checkboxAnswers.push($("#" + $(this).attr("data-antwort-id")).val());
                } else {
                    checkboxAnswers.push($(this).attr("data-antwort-id"));
                }
            });
            question.answer = checkboxAnswers;
        }  // Dropdown
        else if (question.FrageTyp == "c6a0db90-0fe5-4657-afde-450081f7ae9b") {
            question.answer = $(this).find("select").find(":selected").attr("data-antwort-id");
        }
        // Singleline Text Answer
        else if (question.FrageTyp == "65bbbf50-21c0-431b-9f5e-66629c04e2a6") {
            var singleLineAnswers = new Array();
            $(this).find("input[type='text'], input[type='email']").each(function () {
                singleLineAnswers.push($(this).val());
            });
            question.answer = singleLineAnswers;
        }
        answers.push(question);
    });
    return JSON.stringify(answers);
    
    
}

function change(current,nextID,perc)
{
    if(nextID>current)
    {
        //we can store the form already.
        //users love that crash resistent shit
    }
    $("#slide"+current).hide(400);
    $("#slide"+nextID).show(400);
    setStatus(perc);
}

function setStatus(perc)
{
    $("#pbStatus").css("width",perc+"%")
}

$(document).ready(loadPoll);

$(document).ready(function () {
    $('input[type="radio"]').click(function () {
        if($(this).attr('data-fragen-toggle') == 'True'){
            $('#' + $(this).attr('data-fragen-id') + 'show').show();
            $('#' + $(this).attr('data-antwort-id')).focus();
        } else{
            $('#' + $(this).attr('data-fragen-id') + 'show').hide();       
        }
        
    });
});

$(document).ready(function () {
    $('*[id*=show]:visible').each(function () {
        $(this).hide();
    });
});
