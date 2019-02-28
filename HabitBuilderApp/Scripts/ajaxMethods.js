$(function () {


    $('.mark-options__option').on('click', function (e) {
        var $this = $(e.currentTarget);
        var options = $this.closest('.mark-options');
        var id = options.attr('data-identity');
        var day = $('.day[data-identity=' + id + ']');
        day.removeClass('day--success day--fail day--skip day--unmarked');

        if ($this.hasClass('option--fail')) {
            day.addClass('day--fail');
            ChangeStatus(id, "fail");
        } else if ($this.hasClass('option--success')) {
            day.addClass('day--success');
            ChangeStatus(id, "success");
        } else {
            day.addClass('day--skip');
            ChangeStatus(id, "skip");
        }
    });

    function ChangeStatus(id, status) {
        var request = $.ajax(
        {
            type: "POST", //HTTP POST Method  
            url: "Habit/ChangeStatus", // Controller/View   
            data: { //Passing data  
                statusName: status, //Reading text box values using Jquery   
                dayId: id
            }
        });
        request.done(function (msg) {
            console.log($('.day[data-identity=' + id + ']').closest('.card').find('.chain-count__days'));
            $('.day[data-identity=' + id + ']').closest('.card').find('.chain-count__days').text(msg);
        });

        request.fail(function () {
            console.log('failed');
        });

    }; 


    

    $('.not-implemented').on('click', function () {
        alert('Спасибо за проявленный интерес. Эта функция будет добавлена позже!');
    });
});