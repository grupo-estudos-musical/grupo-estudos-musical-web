function apagar(event, id, nome) {
    let modal = $('.modal-body')
    modal.find('input').val(id)

    let modalHeader = $('.modal-header')
    modalHeader.find('.modal-title').text(nome)
}