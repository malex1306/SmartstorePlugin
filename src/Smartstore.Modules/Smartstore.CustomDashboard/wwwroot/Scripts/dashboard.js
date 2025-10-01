//$(document).on('click', '.report-actions .dropdown-item', function (e) {
//    e.preventDefault();

//    var $link = $(this);
//'
//    var newWidgetName = $link.data('widget');

    
//    var $widgetCard = $link.closest('.report');

    
//    if (newWidgetName && $widgetCard.length) {

//        $widgetCard.addClass('loading');

        
//        $.ajax({
//            url: '@Url.Action("GetWidgetHtml", "SwitchAdmin")',
//            type: 'GET',
//            data: { widgetName: newWidgetName },
//            success: function (response) {
//                if (response.widgetHtml) {
                    
//                    $widgetCard.html(response.widgetHtml);
//                } else if (response.error) {
                    
//                    $widgetCard.html('<div class="alert alert-danger">Fehler: ' + response.error + '</div>');
//                }
//            },
//            error: function (xhr, status, error) {
//                $widgetCard.html('<div class="alert alert-danger">AJAX-Fehler: Server nicht erreichbar.</div>');
//            },
//            complete: function () {
//                $widgetCard.removeClass('loading');
//            }
//        });
//    }
//});