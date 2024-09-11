// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    initializeDatatables();

    $('.close-alert').click(function () {
        $('.alert').hide('hide');
    });
});

function initializeDatatables() {
    const tables = ['#tabela-fabricantes', '#tabela-concessionarias', '#tabela-usuarios', '#tabela-veiculos', '#tabela-clientes', '#tabela-vendas'];

    tables.forEach(function (id) {
        $(id).DataTable({
            "ordering": true,
            "paging": true,
            "searching": true,
            "oLanguage": {
                "sEmptyTable": "Nenhum registro encontrado na tabela",
                "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
                "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
                "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
                "sInfoPostFix": "",
                "sInfoThousands": ".",
                "sLengthMenu": "Mostrar _MENU_ registros por página",
                "sLoadingRecords": "Carregando...",
                "sProcessing": "Processando...",
                "sZeroRecords": "Nenhum registro encontrado",
                "sSearch": "Pesquisar",
                "oPaginate": {
                    "sNext": "Próximo",
                    "sPrevious": "Anterior",
                    "sFirst": "Primeiro",
                    "sLast": "Último"
                },
                "oAria": {
                    "sSortAscending": ": Ordenar colunas de forma ascendente",
                    "sSortDescending": ": Ordenar colunas de forma descendente"
                }
            }
        });
    });
}

$(document).ready(function () {
    $('#FabricanteID').change(function () {
        var fabricanteId = $(this).val();

        if (fabricanteId) {
            $.ajax({
                url: '/Vendas/BuscarPorFabricante',
                type: 'GET',
                data: { fabricanteId: fabricanteId },
                success: function (response) {
                    if (response.sucesso) {
                        var veiculoSelect = $('#veiculoSelect');
                        veiculoSelect.empty();

                        veiculoSelect.append('<option value="">Selecione um modelo</option>');

                        $.each(response.data, function (index, modelo) {
                            veiculoSelect.append($('<option>', {
                                value: modelo.value,
                                text: modelo.text
                            }));
                        });
                    } else {
                        alert(response.mensagem);
                    }
                },
                error: function () {
                    alert('Erro ao buscar os modelos.');
                }
            });
        } else {
            $('#veiculoSelect').empty();
            $('#veiculoSelect').append('<option value="">Selecione um fabricante primeiro</option>');
        }
    });
});

