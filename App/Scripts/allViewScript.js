var dragAndDropModule = (function () {
    var CONSTANTS = {
        TYPE: 'POST',
        CONTENTTYPE: 'application/json; charset=utf-8',
        DATATYPE: 'json',
        FILTER: {
            STATUS: 1,
            PRIORITY: 2,
            REQUEST: 3
        }
    }

    var variables = {
        statusurl: '',
        sameContainer: true,
        sourceParent: ''
    }

    function privateUpdateTickets(tickets) {
        $.ajax({
            type: CONSTANTS.TYPE,
            contentType: CONSTANTS.CONTENTTYPE,
            dataType: CONSTANTS.DATATYPE,
            url: variables.statusurl,
            data: JSON.stringify(tickets),
            success: function (data) {
                console.log(data);
            },
            error: function (err) {
                console.error(err);
            }
        });
    }

    function privateStartSortable(filterType) {
        $('.sortable').sortable({
            connectWith: '.sortable',
            start: function(event, ui) {
                var ticketItem = $(ui.item[0]);
                variables.sourceParent = ticketItem.parent().siblings().eq(0).find('.mt0');
            },
            receive: function (event, ui) {
                var ticketItem = $(ui.item[0]);
                var target = $(event.target);
                var newHeading = '';
                var ticket = {
                    id: ticketItem.data('id'),
                    description: ticketItem.find('.js-ticket-description').text().trim(),
                    tittle: ticketItem.find(".js-ticket-tittle").text().trim()
                };

                switch (filterType) {
                    case CONSTANTS.FILTER.STATUS:
                        ticket.statusTypeId = target.siblings().eq(0).data('status-id');
                        ticket.requestTypeId = ticketItem.data('rq-id');
                        ticket.priorityTypeId = ticketItem.data('pt-id');
                        newHeading = target.siblings().eq(0).data('status-desc');                        
                        ticketItem.find('.panel-heading').children().text(newHeading);
                        break;
                    case CONSTANTS.FILTER.PRIORITY:
                        ticket.statusTypeId = ticketItem.data('status-id');
                        ticket.requestTypeId = ticketItem.data('rq-id');
                        ticket.priorityTypeId = target.siblings().eq(0).data('pt-id');

                        newHeading = target.siblings().eq(0).data('pt-desc');
                        ticketItem.find('.panel-heading').children().text(newHeading);
                        break;
                    case CONSTANTS.FILTER.REQUEST:
                        ticket.statusTypeId = ticketItem.data('status-id');
                        ticket.requestTypeId = target.siblings().eq(0).data('rq-id');
                        ticket.priorityTypeId = ticketItem.data('pt-id');


                        newHeading = target.siblings().eq(0).data('rq-desc');
                        ticketItem.find('.panel-heading').children().html('<label>Tipo de solicitud: </label> ' + newHeading);
                        break;
                }

                alertify.log("El ticket #: " + ticket.id + " se movió de <b style=\"color:white;\"> " + ticket.tittle + "</b>"+ " a " + "<b style=\"color:white;\">" + newHeading + "</b>");

                // update ticket count on target
                var ticketCount = parseInt(target.siblings().eq(0).find('.mt0').text());
                target.siblings().eq(0).find('.mt0').text(ticketCount + 1);
                variables.sameContainer = false;
                privateUpdateTickets(ticket);

            },
            stop: function (event, ui) {
                if (!variables.sameContainer) {
                    // update ticket count on source
                    var ticketCount = parseInt(variables.sourceParent.text());
                    variables.sourceParent.text(ticketCount - 1);
                    variables.sameContainer = true;
                }
            }
        });
    }

    function privateSelectMenuItem() {
        $('#All').addClass('active2');
    }

    function privateCountTickets() {
        var column = $('.col');
        
        for(var i = 0; i < column.length; i++)
        {
            var hiddenTickets = column.eq(i).find('.ticket-hidden').length
            var allTickets = column.eq(i).find('.panel-body').length;
            var tickets = allTickets - hiddenTickets;
            column.eq(i).find('.js-ticket-count').text(tickets);
        }
    }

    function privateSearchByUser() {
        $('.search').on('keyup', function () {
            var $this = $(this);
            var panels = $('.panel-primary');

            if ($this.val().trim().length > 0) {
                panels.each(function (index, element) {
                    if (panels.eq(index).data('author').toLowerCase().indexOf($this.val().toLowerCase()) >= 0) {
                        panels.eq(index).removeClass('ticket-hidden');
                    } else {
                        panels.eq(index).addClass('ticket-hidden');
                    }
                });
            } else {
                panels.each(function (index, element) {
                    if (panels.eq(index).hasClass('on-screen-ticket')) {
                        panels.eq(index).removeClass('ticket-hidden');
                    } else {
                        panels.eq(index).addClass('ticket-hidden');                        
                    }
                });
            }
            privateCountTickets();
        });
    }

    return {
        getFilterEnum: function () {
            return CONSTANTS.FILTER;
        },
        setUpdateTicketsUrl: function (url) {
            variables.statusurl = url;
        },
        updateStatus: function (ticket) {
            if (variables.statusurl) {
                privateUpdateTickets(ticket);
            }
            else {
                console.error('no Updatetickets url has been set');
            }
        },
        startSortable: function (filterType) {
            if (variables.statusurl) {
                privateStartSortable(filterType);
            }
            else {
                console.log("Status url has not been set");

            }
        },
        countTickets: function () {
            privateSelectMenuItem();
        },
        init: function () {
            privateSelectMenuItem();
            privateCountTickets();
            privateSearchByUser();
        }
    }
})();

$(function () {

    $('#result').on('click', '.js-clickable-ticket', function () {
        var $this = $(this);
        var id = $this.attr('data-id')
        var url = 'Detail/' + id;
        window.location = url;
    });

    dragAndDropModule.startSortable(dragAndDropModule.getFilterEnum().STATUS);

    $('.navbar-nav > li').on('click', function () {
        var $this = $(this);
        $this.addClass('active2');
        $this.siblings().removeClass('active2');
    });

    
});