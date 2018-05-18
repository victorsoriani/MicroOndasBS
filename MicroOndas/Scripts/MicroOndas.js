var potencia = 0;
var contador = 0;
var timeout;

function controleBotoes(status) {
    $("#btnEnviarParametrosEntradas").prop('disabled', status);
    $("#btnInicioRapido").prop('disabled', status);
    $("#btnListarFuncoes").prop('disabled', status);
}

$(document).ready(function () {
    $("#btnEnviarParametrosEntradas").click(function () {
        controleBotoes(true);
        $.ajax({
            type: "POST",
            url: '/MicroOndas/ResultadoTempoMicroOndas',
            contentType: "application/json",
            dataType:'json',
            data: JSON.stringify( { parametrosEntradas: $("#parametrosEntradas").val() }),
            success: function (result) {
                if (result.erro != null && result.erro != "") {
                    alerta(result.msgerro);
                    controleBotoes(false);
                } else {
                    $("#parametrosEntradas").val(result.parametrosEntradas);
                    potencia = result.potencia;
                    startPontos(result.caractere);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alerta("Ocorreu um erro não indentificado no sistema!");
            }
        });
    });

    $("#btnInicioRapido").click(function () {
        controleBotoes(true);
        $.ajax({
            type: "POST",
            url: '/MicroOndas/ResultadoTempoMicroOndas',
            contentType: "application/json",
            dataType: 'json',
            data: JSON.stringify({ parametrosEntradas: "30,8" }),
            success: function (result) {
                if (result.erro != null && result.erro != "") {
                    alerta(result.msgerro);
                    controleBotoes(false);
                } else {
                    $("#parametrosEntradas").val(result.parametrosEntradas);
                    potencia = result.potencia;
                    startPontos(result.caractere);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alerta("Ocorreu um erro não indentificado no sistema!");
            }
        });
    });
    $("#btnListarFuncoes").click(function () {
        controleBotoes(true);
        $.ajax({
            type: "GET",
            url: '/MicroOndas/ListaProgramasPreDefinidos',
            contentType: "application/json",
            dataType: 'json',
            success: function (result) {
                if (result.erro != null && result.erro != "") {
                    alerta(result.msgerro);
                    controleBotoes(false);
                } else {
                    montaListaProgramas(result);
                    controleBotoes(false);
                    $("#btnListarFuncoes").prop('disabled', true);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alerta("Ocorreu um erro não indentificado no sistema!");
            }
        });
    });

    $("#listaPreDefinicoes").on('click', 'a', function () {
        $("#listaPreDefinicoes a").removeClass('active');
        $(this).addClass('active');
        $("#parametrosEntradas").val($(this).attr('string'));
        $("#listaPreDefinicoes").fadeOut(300).html('');
        $("#btnEnviarParametrosEntradas").trigger('click');
    });

    $("#listaPreDefinicoes").on('click', '#btnPesquisar', function () {
        $("#listaDeFuncoes").find("a:not([string=" + $("#pesquisaProdutos").val() + "])").fadeOut(300);
    });

    $("#listaPreDefinicoes").on('click', '#btnLimpar', function () {
        $("#listaDeFuncoes").find("a").fadeIn(300);
    });
});

function startPontos(caractere) {
    timeout = setTimeout(function () {
        if (contador == potencia) {
            clearTimeout(timeout);
            finalizadoAquecimento();
        } else {
            $("#parametrosEntradas").val($("#parametrosEntradas").val() + caractere);
            contador++;
            startPontos(caractere);
        }
    }, 1000)
}

function finalizadoAquecimento() {
    alerta("Finalizado o aquecimento!");
    potencia = 0;
    contador = 0;
    timeout = null;
    controleBotoes(false);
}

function alerta(msg) {
    $(".alert").fadeOut(400, function () { $(this).text(msg).fadeIn(500) });
}

function montaListaProgramas(result) {
    var divLista = $('<div id="listaDeFuncoes" class="list-group">'+
                        '<span>'+
                            '<input class="form-control" id="pesquisaProdutos" name="pesquisaProdutos" style="display:inline; margin-right:20px;" placeholder="pesquise..." type="text">' +
                            '<button type="button" id="btnPesquisar" class="btn btn-primary">Pesquisar</button>' +
                            '<button type="button" id="btnLimpar" class="btn btn-danger">Limpar</button>' +
                        '</span>' +
                     '</div>');
    $("#listaPreDefinicoes").append(divLista);

    for (var i = 0; i < result.length ; i++) {
        var a = $('<a href="#" class="list-group-item list-group-item-action flex-column align-items-start" string="' + result[i].nome + '" caractere="' + result[i].caractere + '"></a>');
        
        var divItem=$('<div class="d-flex w-100 justify-content-between"></div>');
        $(divItem).append('<h5 class="mb-1">' + result[i].nome + '</h5><small>Tempo:'+result[i].tempo+'s / Potência: '+result[i].potencia+'</small>');
        $(a).append(divItem);

        $(a).append('<p class="mb-1">' + result[i].instrucaoDeUso + '</p><small>Preenchimento da string: ' + result[i].caractere + '</small>');
        
        $("#listaPreDefinicoes>div").append(a);
    }
    $("#listaPreDefinicoes").fadeIn(200);
}