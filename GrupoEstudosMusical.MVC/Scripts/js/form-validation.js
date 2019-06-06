$(document).ready(function() {


    if ($.isFunction($.fn.validate)) {

        $('#alterar_senha').validate({
            focusInvalid: false,
            ignore: "",
            rules: {
                NovaSenha: {
                    required: true,
                    maxlength: 8,
                    minlength: 6
                }
            },
            highlight: function (element) { // hightlight error inputs
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            }
        });

        $('#loginform').validate({
            focusInvalid: false,
            ignore: "",
            rules: {
                Email: {
                    required: true
                },
                Senha: {
                    required: true
                }
            },
            highlight: function (element) { // hightlight error inputs
                var parent = $(element).parent().parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            }
        });

        $('#form_aula').validate({
            focusInvalid: false,
            ignore: "",
            rules: {
                Conteudo: {
                    required: true,
                    maxlength: 300
                }
            },
            highlight: function (element) { // hightlight error inputs
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            }
        });

        $('#form_emprestimoAluno').validate({
            focusInvalid: false,
            ignore: "",
            rules: {
                InventarioID: {
                    required: true
                },
                FabricanteID: {
                    required: true
                },
                Cor: {
                    required: true,
                    maxlength: 10
                },
                DataEmprestimo: {
                    required: true,
                    date: true
                },
                AnoDeFabricacaoInstrumento: {
                    required: true,
                    maxlength: 4,
                    minlength: 4
                }
            },
            highlight: function (element) { // hightlight error inputs
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            }
        });




        $('#ocorrencia_validate').validate({
            focusInvalid: false,
            ignore: "",
            rules: {
                Titulo: {
                    maxlength: 70,
                    required: true
                },
                DataOcorrido: {
                    required: true
                },
                Resumo: {
                    maxlength:300,
                    required: true
                },
                TipoID: {
                    required: true
                }



            },


            highlight: function (element) { // hightlight error inputs
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            },

            // unhighlight: function(element) { // revert the change done by hightlight

            // },

            success: function (label, element) {
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-error').addClass('has-success');
            },

        });

        $('#avaliacao_validate').validate({
            focusInvalid: false,
            ignore: "",
            rules: {
                Nome: {
                    maxlength: 100,
                    required: true
                },
                NotaMaxima: {
                    required: true,
                    maxlength: 300,
                    minlength: 1
                },
                Peso: {
                    required: true,
                    maxlength: 10,
                    minlength : 1
                }


            },


            highlight: function (element) { // hightlight error inputs
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            },

            // unhighlight: function(element) { // revert the change done by hightlight

            // },

            success: function (label, element) {
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-error').addClass('has-success');
            },

        });

        $('#fabricantes_validate').validate({
            focusInvalid: false,
            ignore: "",
            rules: {
                Nome: {
                    maxlength: 100,
                    required: true
                }
            },


            highlight: function (element) { // hightlight error inputs
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            },

            // unhighlight: function(element) { // revert the change done by hightlight

            // },

            success: function (label, element) {
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-error').addClass('has-success');
            },

        });





        $('#msg_validate').validate({
            focusInvalid: false,
            ignore: "",
            rules: {
                formfield1: {
                    minlength: 2,
                    required: true
                },
                formfield2: {
                    required: true,
                    email: true
                },
                formfield3: {
                    required: true,
                    url: true
                }
            },

            invalidHandler: function(event, validator) {
                //display error alert on form submit    
            },

            errorPlacement: function(label, element) { // render error placement for each input type   
                console.log(label);
                $('<span class="error"></span>').insertAfter(element).append(label)
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            },

            highlight: function(element) { // hightlight error inputs
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            },

            unhighlight: function(element) { // revert the change done by hightlight

            },

            success: function(label, element) {
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-error').addClass('has-success');
            },

            submitHandler: function(form) {

            }
        });

        $('#turma_validate').validate({
            focusInvalid: false,
            ignore: "",
            rules: {
                Nome: {
                    maxlength: 60,
                    required:true
                },
                QuantidadeAlunos: {
                    min: 1,
                    required:true
                }


            },


            highlight: function (element) { // hightlight error inputs
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            },

            // unhighlight: function(element) { // revert the change done by hightlight

            // },

            success: function (label, element) {
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-error').addClass('has-success');
            },

        });


        $('#icon_validate').validate({
            errorElement: 'span',
            errorClass: 'error',
            focusInvalid: false,
            ignore: "",
            rules: {
                formfield1: {
                    minlength: 2,
                    required: true
                },
                formfield2: {
                    required: true,
                    email: true
                },
                formfield3: {
                    required: true,
                    url: true
                }
            },

            invalidHandler: function(event, validator) {
                //display error alert on form submit    
            },

            errorPlacement: function(error, element) { // render error placement for each input type
                var icon = $(element).parent().parent('.form-group').find('i');
                var parent = $(element).parent().parent('.form-group');
                icon.removeClass('fa fa-check').addClass('fa fa-times');
                parent.removeClass('has-success').addClass('has-error');
            },

            highlight: function(element) { // hightlight error inputs
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            },

            unhighlight: function(element) { // revert the change done by hightlight

            },

            success: function(label, element) {
                var icon = $(element).parent().parent('.form-group').find('i');
                var parent = $(element).parent().parent('.form-group');
                icon.removeClass("fa fa-times").addClass('fa fa-check');
                parent.removeClass('has-error').addClass('has-success');
            },

            submitHandler: function(form) {

            }
        });

        jQuery.validator.addMethod("cpf", function(value, element) {
            value = jQuery.trim(value);
         
             value = value.replace('.','');
             value = value.replace('.','');
             cpf = value.replace('-','');
             while(cpf.length < 11) cpf = "0"+ cpf;
             var expReg = /^0+$|^1+$|^2+$|^3+$|^4+$|^5+$|^6+$|^7+$|^8+$|^9+$/;
             var a = [];
             var b = new Number;
             var c = 11;
             for (i=0; i<11; i++){
                 a[i] = cpf.charAt(i);
                 if (i < 9) b += (a[i] * --c);
             }
             if ((x = b % 11) < 2) { a[9] = 0 } else { a[9] = 11-x }
             b = 0;
             c = 11;
             for (y=0; y<10; y++) b += (a[y] * c--);
             if ((x = b % 11) < 2) { a[10] = 0; } else { a[10] = 11-x; }
         
             var retorno = true;
             if ((cpf.charAt(9) != a[9]) || (cpf.charAt(10) != a[10]) || cpf.match(expReg)) retorno = false;
         
             return this.optional(element) || retorno;
         
         }, "Informe um CPF válido");
       
        $('#general_validate').validate({
            focusInvalid: false,
            ignore: "",
            rules: {
                Nome: {
                    required: true
                },
                DataNascimento: {
                    required: true
                },
                Email: {
                    email: true,
                    required: true
                },
                Logradouro: {                    
                    required: true
                },
                Bairro: {
                    required: true
                },
                Cidade: {
                    required: true
                },
                Uf: {
                    required: true
                },
                Cpf: {
                    cpf: true
                },
                Observacoes: {
                    required: true
                },
                Numero: {
                    required: true
                },
                TipoResponsavel: {
                    required: true
                }
            },



            // invalidHandler: function(event, validator) {
            //     //display error alert on form submit    
            // },

            // errorPlacement: function(label, element) { // render error placement for each input type   
            //     console.log(label);
            //     $('<span class="error"></span>').insertAfter(element).append(label)
            //     var parent = $(element).parent().parent('.form-group');
            //     parent.removeClass('has-success').addClass('has-error');
            // },

            highlight: function(element) { // hightlight error inputs
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            },

            // unhighlight: function(element) { // revert the change done by hightlight

            // },

            success: function(label, element) {
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-error').addClass('has-success');
            }

            // submitHandler: function(form) {

            // }
        });

        $('#modulo_validate').validate({
            focusInvalid: false,
            ignore: "",
            rules: {
                nome: {
                    required: true,
                    maxlength: 60
                },
                detalhes: {
                    required: false,
                    maxlength: 300
                }
            },
            highlight: function(element) { // hightlight error inputs
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-success').addClass('has-error');
            },
            success: function(label, element) {
                var parent = $(element).parent().parent('.form-group');
                parent.removeClass('has-error').addClass('has-success');
            }
        });

        jQuery.validator.addMethod('turma', function (value, element) {
            value = jQuery.trim(value);

            retorno = false;

            if (value !== "0") {
                retorno = true;
            }

            return this.optional(element) || retorno;
        }, "Por favor selecione uma Turma.");

        //Form Wizard Validations
        var $validator = $("#commentForm").validate({
            ignore: "",
            rules: {
                txtFullName: {
                    required: true,
                    minlength: 3
                },
                txtEmail: {
                    required: true,
                    email: true,
                },
                txtPhone: {
                    number: true,
                    required: true,
                },
                TurmaId: {
                    turma: true
                }
            },
            errorPlacement: function(label, element) {
                $('<span class="arrow"></span>').insertBefore(element);
                $('<span class="error"></span>').insertAfter(element).append(label);
            }
        });


    }



    if ($.isFunction($.fn.bootstrapWizard)) {

        $('#pills').bootstrapWizard({
            'tabClass': 'nav nav-pills',
            'debug': false,
            onShow: function(tab, navigation, index) {
                console.log('onShow');
            },
            onNext: function(tab, navigation, index) {
                //console.log('onNext');
                if ($.isFunction($.fn.validate)) {
                    var $valid = true;
                    if (index === 3) {
                        $valid = $("#commentForm").valid();
                    }
                    if (!$valid) {
                        $validator.focusInvalid();
                        return false;
                    } else {
                        $('#pills').find('.form-wizard').children('li').eq(index - 1).addClass('complete');
                        $('#pills').find('.form-wizard').children('li').eq(index - 1).find('.step').html('<i class="fa fa-check"></i>');
                    }
                }
            },
            onPrevious: function(tab, navigation, index) {
                console.log('onPrevious');
            },
            onLast: function(tab, navigation, index) {
                console.log('onLast');
            },
            onTabClick: function(tab, navigation, index) {
                console.log('onTabClick');
                //alert('on tab click disabled');
            },
            onTabShow: function(tab, navigation, index) {
                console.log('onTabShow');
                var $total = navigation.find('li').length;
                var $current = index + 1;
                var $percent = ($current / $total) * 100;
                $('#pills .progress-bar').css({
                    width: $percent + '%'
                });
                $('.next').removeClass('disabled');
                //if ($current === 3) {
                //    $('#TurmaId').removeAttr('value');
                //}
            }
        });

        $('#pills .finish').click(function() {
            alert('Finished!, Starting over!');
            $('#pills').find("a[href*='tab1']").trigger('click');
        });







    }




});
