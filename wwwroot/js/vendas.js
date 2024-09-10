$(document).ready(function () {
    $('#fabricanteSelect').change(function () {
        var fabricanteId = $(this).val();
        if (fabricanteId) {
            $.ajax({
                url: '/Vendas/BuscarPorFabricante', 
                type: 'GET',
                data: { fabricanteId: fabricanteId },
                success: function (data) {
                    var $veiculoSelect = $('#veiculoSelect');
                    $veiculoSelect.empty();
                    $veiculoSelect.append('<option value="">Selecione um modelo</option>');

                    $.each(data, function (index, item) {
                        $veiculoSelect.append($('<option></option>').val(item.value).text(item.text));
                    });
                },
                error: function (xhr, status, error) {
                    console.error('Erro ao carregar veículos:', error);
                }
            });
        } else {
            $('#veiculoSelect').empty().append('<option value="">Selecione um modelo</option>');
        }
    });
});
