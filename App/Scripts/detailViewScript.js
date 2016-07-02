/// <reference path="C:\Users\rio\Desktop\github\tickets\App\Views/Ticket/All.cshtml" />
var detailModule = (function () {
    var CONSTANTS = {
        TYPE: 'POST',
        CONTENTTYPE: 'application/json; charset=utf-8',
        DATATYPE: 'json'
    }

    var variables = {
        statusUrl: '',
        commentUrl: '',
        rateUrl: '',
        userName: '',
        ownerUrl: ''
    }

    function privateUpdateStatus(ticket) {
        $.ajax({
            type: CONSTANTS.TYPE,
            contentType: CONSTANTS.CONTENTTYPE,
            dataType: CONSTANTS.DATATYPE,
            url: variables.statusUrl,
            data: JSON.stringify(ticket),
            success: function (data) {
                console.log(data);
            },
            error: function (err) {
                console.error(err);
            }
        });
    }

    function privateInsertNewComment(comment) {
        $.ajax({
            type: CONSTANTS.TYPE,
            contentType: CONSTANTS.CONTENTTYPE,
            dataType: CONSTANTS.DATATYPE,
            url: variables.commentUrl,
            data: JSON.stringify(comment),
            success: function (data) {
                console.log(data);
                var commentContainer = privateCreateCommentElement(comment.comment);
                //commentContainer.insertAfter("#Top");
                $('.js-comment').val(' ');
            },
            error: function (err) {
                console.error(err);
            }
        })
    }
    
    function privateUpdateOwner(ticket){
        $.ajax({
            type: CONSTANTS.TYPE,
            contentType: CONSTANTS.CONTENTTYPE,
            dataType: CONSTANTS.DATATYPE,
            url:variables.ownerUrl,
            data: JSON.stringify(ticket),
            success: function(data){
                console.log(data);
                $('#TakeTicket').hide();
            },
            error: function (err) {
                console.error(err);
            }
        })
    }

    function privateCreateCommentElement(comment) {
        var commentContainer = $('<a/>', { 'class': 'list-group-item' });
        var divCotainer = $('<div/>', { 'class': 'media-box' });
        var divContainer2 = $('<div/>', { 'class': 'media-box-body clearfix' })
        var smallDateContainer = $('<small/>', { 'class': 'pull-right' });
        var strongCommentContainer = $('<strong/>', { 'class': 'media-box-heading text-primary' });
        var sampContainer = $('<samp/>', { 'class': 'text-left' });
        var pcontainer = $('<p/>', { 'class': 'mb-sm' });
        var smallcontainer = $('<small/>');

        //var commentbox = $('<div/>', { 'class': 'list-group' });
        //var commentboxparent = $('<div/>',{'class':'slimScrollDiv'});
        var commentpanel = $('.comentarios');

        smallDateContainer.text(privateGetDateString());
        sampContainer.text(variables.userName);

        pcontainer.append(smallcontainer);
        smallcontainer.append(comment);
        strongCommentContainer.append(sampContainer);

        divContainer2.append(smallDateContainer);
        divContainer2.append(strongCommentContainer);
        divContainer2.append(pcontainer);

        divCotainer.append(divContainer2);
        commentContainer.append(divCotainer);
        
        //commentContainer.append(commentbox);
        //commentContainer.append(commentboxparent);
        //commentContainer.append(commentpanel);
    
        commentpanel.prepend(commentContainer);
        return commentContainer;

    }

    function privateGetDateString() {
        var date = new Date();
        var dateString = [];
        dateString.push(date.getMonth() + 1);
        dateString.push("/");
        dateString.push(date.getDate());
        dateString.push("/");
        dateString.push(1900 + date.getYear());
        dateString.push(" ");
        dateString.push(date.getHours() > 12 ? date.getHours() - 12 : date.getHours());
        dateString.push(":");
        dateString.push(date.getMinutes());
        dateString.push(":");
        dateString.push(date.getSeconds());
        dateString.push(" ");
        dateString.push(date.getHours() > 12 ? "PM" : "AM");
        return dateString.join('');
    }

    function privateUpddateRate(rate) {
        $.ajax({
            type: CONSTANTS.TYPE,
            contentType: CONSTANTS.CONTENTTYPE,
            dataType: CONSTANTS.DATATYPE,
            url: variables.rateUrl,
            data: JSON.stringify(rate),
            success: function (data) {
                console.log(data);
            },
            error: function (err) {
                console.error(err);
            }
        })
    }

    return {
        setUserName: function (userName) {
            variables.userName = userName;
        },
        setStatusUrl: function (url) {
            variables.statusUrl = url;
        },
        setCommentUrl: function (url) {
            variables.commentUrl = url;
        },
        setRateUrl: function (url) {
            variables.rateUrl = url;
        },
        setOwnerUrl: function (url) {
            variables.ownerUrl = url;
        },

        UpdateRate: function (ticket) {
            if (variables.rateUrl) {
                privateUpddateRate(ticket);
            }
            else {
                console.error('Update rate has not been set');
            }
        },
        updateStatus: function (ticket) {
            if (variables.statusUrl) {
                privateUpdateStatus(ticket);
            }
            else {
                console.error('no status url has been set');
            }
        },
        updateOwner: function(ticket) {
            if(variables.ownerUrl){
                privateUpdateOwner(ticket)
                $("#StatusDropdown").val($("#StatusDropdown option:eq(1)").val());
            }
            else{
                console.error('no owner url has been set');
            }
        },
        insertComment: function (comment) {
            if (variables.commentUrl) {
                privateInsertNewComment(comment);
            }
            else {
                console.error('no status url has been set');
            }
        }
    }
})();

// JQuery code
$(function () {
    
    $("#Ticket_StatusTypeId").change(function () {
        swal({
            title: "Desea cambiar el estatus del ticket?",
            text: "Esta seguro?",
            type: "info",
            showCancelButton: true,
            confirmButtonText: "Claro!",
            cancelButtonText: "Cancelar",
            closeOnConfirm: false,
            closeOnCancel: false
        },

        function (isConfirm) {
            if (isConfirm) {
                var $this = $(this);
                var ticket = {
                    id: $('.ticket-id').attr('data-id'),
                    statusTypeId: $('#Ticket_StatusTypeId option:selected').val(),
                    description: $('.ticket-description').text().trim(),
                    priorityTypeId: $('.js-priority-id').attr('data-id'),
                    requestTypeId: $('.js-request-id').attr('data-id')

                };
                detailModule.updateStatus(ticket);
                swal("¡Hecho!", "Se ha cambiado el estatus", "success");
                
            } else {
                swal("¡Cancelar!", "", "error");
            }
        });

    });

    $('#InsertComment').on('click', function () {
        var commentTextBox = $('#TicketComment_Comment');
        var comment = {
            ticketId: $('.ticket-id').attr('data-id'),
            comment: commentTextBox.val()

        };
        if (comment.comment.trim()) {
            detailModule.insertComment(comment);
        }
        else {
            var span = $('.field-validation-valid');
            span.addClass('field-validation-error').removeClass('field-validation-valid');
            var innerSpan = $('<span/>', { 'text': 'Por favor ingrese un comentario.' });
            span.append(innerSpan);
            span.insertAfter(commentTextBox);
        }
    });

    //Star Ranking.
    var ratecurrentval = $(".js-rate").data('rate')
    $(".js-rate").rateYo({
        fullStar: true,
        starWidth: "20px",
        rating: ratecurrentval,
        onSet: function (rate, rateYoInstance) {
            var ticket = {
                Id: $('.ticket-id').data('id'),
                Rate: rate,
            }
            detailModule.UpdateRate(ticket)
            swal("¡Gracias!", "Gracias por tu opinion:) ", "success");

            //swal("Gracias por tu opinion!: " + ticket.Rate);
        }
    });

    //Star Ranking admin
    var ratecurrentvala = $(".js-rate-admin").data('rate')
    $(".js-rate-admin").rateYo({
        fullStar: true,
        starWidth: "20px",
        rating: ratecurrentvala,
        readOnly: true,
    });

     $(document).ready(function () {
         //x = 0;
         $("#scroll").scroll(function () {
             //$("#comments").text(x+=1)
         });
     });

    //ScrollUp
    $('a[href^="#"]').on('click', function (event) {

        var target = $($(this).attr('href'));

        if (target.length) {
            event.preventDefault();
            $('html, body').animate({
                scrollTop: target.offset().top
            }, 1000);
        }

    });
    
    $('#TakeTicket').on('click', function(){
        var takeTicket = $('#TakeTicket');
        var ticket = {
                Id: takeTicket.data('id')
            }
        detailModule.updateOwner(ticket);
        
    });

});
