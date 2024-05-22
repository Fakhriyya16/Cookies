$(document).ready(function () {

    $(document).on("click", "#products .add-basket", function () {
        let id = parseInt($(this).attr("data-id"));

        $.ajax({
            type: "POST",
            url: `home/addproducttobasket?id=${id}`,
            success: function (response) {
                $(".rounded-circle").text(response.count);
                $(".rounded-circle").next().text(`CART ($${response.total})`);
            }
        })
    })

    //$(document).on("click", ".delete-product", function () {
    //    let id = parseInt($(this).attr("data-id"));

    //    $.ajax({
    //        type: "POST",
    //        url: `cart/delete?id=${id}`,
    //        success: function (response) {
    //            console.log("test");
    //            $(".rounded-circle").text(response.count);
    //            $(".rounded-circle").next().text(`CART ($${response.total})`);
    //        }
    //    })
    //})
})