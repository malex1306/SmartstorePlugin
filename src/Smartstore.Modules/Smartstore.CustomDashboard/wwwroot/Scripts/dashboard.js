$(document).ready(function () {
    $(document).on('change', '.widget-chooser', function () {
        var $select = $(this);
        var widgetName = $select.val();
        var $cell = $select.closest('.grid-cell');
        var row = $cell.data('row');
        var col = $cell.data('col');

        if (widgetName) {
            $.get('@Url.Action("GetWidgetHtml", "DashboardAdmin")', { widgetName: widgetName })
                .done(function (data) {
                    if (data.widgetHtml) {
                        $cell.html(data.widgetHtml);

                        
                        $cell.css('grid-column', `span ${data.widgetWidth}`);

                        var nextCol = col + data.widgetWidth;
                        if (nextCol <= 12) {
                            var nextCellHtml = `
                                <div class="grid-cell" data-row="${row}" data-col="${nextCol}">
                                    <select class="widget-chooser form-control">
                                        <option value="">Widget auswählen...</option>
                                        <option value="Payments">Payments</option>
                                        <option value="LastContacts">Letzte Kontakte</option>
                                        <option value="Bestsellers">Top Bestsellers</option>
                                        <option value="TopCustomers">Top Kunden</option>
                                        <option value="CustomerRegistrations">Neuanmeldungen</option>
                                        <option value="LatestOrders">Letzte Bestellungen</option>
                                    </select>
                                </div>
                            `;
                            $cell.after(nextCellHtml);
                        }
                    }
                });
        }
    });
});