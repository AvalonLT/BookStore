$(document).ready(function () {
    $("#books .js-remove").on("click", function () {
        var button = $(this);

        if (confirm("Remove?")) {
            $.ajax({
                url: "/api/basket/" + button.attr("data-book-id"),
                method: "POST",
                success: function () {
                    button.parent("div").parent("div").remove();
                }
            });
        }
    });
});

$(document).ready(function () {
    $("#menu-basket").text('Basket');
});