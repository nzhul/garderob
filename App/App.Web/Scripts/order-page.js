$(document).ready(function () {
	// sticky basket
	if ($(window).width() > 1200) {
		var cartContainer = $('#order-container');
		var originalLeftPosition = cartContainer.offset().left;
		var originalWidth = cartContainer.outerWidth();

		$(window).scroll(function () {
			var documentScrollPosition = $(document).scrollTop();
			if (600 < documentScrollPosition) {
				if (!cartContainer.hasClass('sticky-cart')) {
					cartContainer.addClass('sticky-cart');
					cartContainer.css('left', originalLeftPosition);
					cartContainer.css('width', originalWidth);
				}
			} else {
				if (cartContainer.hasClass('sticky-cart')) {
					cartContainer.removeClass('sticky-cart');
					cartContainer.css('left', '-50px');
				}
			}
		})
	}

	// basket logic
	updateBasketView();

});

function updateBasketView() {
	var basketEmptyView = $('.basket-empty-view');
	var basketFullVIew = $('.basket-full-view');
	var basketItemsContainer = $('.basket-items-container');

	if (basketItemsContainer.children().length > 0) {

		calculateAndUpdateTotalPrice(basketItemsContainer.children());
		basketFullVIew.show();
		basketEmptyView.hide();
	} else {
		basketFullVIew.hide();
		basketEmptyView.show();
	}
}

function appendNewBasketRow(data) {
	if (data) {
		debugger;
		var basketItemsContainer = $('.basket-items-container');
		var title = data.title;
		var installation = data.installation == true ? "Да" : "Не";
		var price = data.price;
		var id = data.id;
		var count = data.count;

		var rowContent = '<tr><td>' + title + '</td><td class="js-installation" style="text-align:center;">' + installation + '</td><td class="js-count">' + count + '</td><td class="js-price">' + price + '</td><td><a class="fa fa-remove" data-ajax="true" data-ajax-begin="removeFromCartBegin" data-ajax-failure="removeFromCartFail" data-ajax-method="Post" data-ajax-success="removeFromCartSuccess" href="/warehouse/removecartitem?orderId=' + id + '" data-orderId="' + id + '"></a></td></tr>';
		var newRow = $(rowContent);
		basketItemsContainer.append(newRow);
	}
}

function calculateAndUpdateTotalPrice(products) {
	var totalPrice = 0;
	var totalInstallationPrice = 0;
	var totalFinalPrice = 0;
	var installationPercent = 7; // TODO: check if this value is the right one

	for (var i = 0; i < products.length; i++) {
		debugger;
		var productRow = $(products[i]);
		var installation = productRow.children('td.js-installation').html().trim() === 'Да' ? true : false;
		var count = productRow.children('td.js-count').html();
		var price = parseFloat(productRow.children('td.js-price').html());

		price = price * count;

		if (installation) {
			totalInstallationPrice += (price * installationPercent) / 100;
		}

		totalPrice += price;
	}

	totalFinalPrice = totalPrice + totalInstallationPrice;

	$('.js-total-price').html(totalPrice);
	$('.js-total-installation-price').html(totalInstallationPrice);
	$('.js-total-final-price').html(totalFinalPrice);
}

// ajaxActionLink functions:
function addToCartBegin(xhr, request) {
	var clickedBtn = $(this);
	var orderId = clickedBtn.data('orderid');
	var nearestLoadingElement = clickedBtn.prev('.add-cart-loading');

	clickedBtn.removeAttr('href');
	clickedBtn.css('opacity', '0.5');
	nearestLoadingElement.show();

	var requireInstallation = $('#installationcb-' + orderId).is(":checked");
	var orderCount = $('#order-count-ddl-' + orderId).val();

	request.url = request.url + '&orderCount=' + orderCount + '&installation=' + requireInstallation;
}

function addToCartFail() {
	var clickedBtn = $(this);
	var nearestLoadingElement = clickedBtn.prev('.add-cart-loading');
	var nearestFailElement = clickedBtn.prevAll('.add-cart-fail');

	clickedBtn.hide();
	nearestLoadingElement.hide();
	nearestFailElement.show();
}

function addToCartSuccess(response) {
	var clickedBtn = $(this);
	var nearestLoadingElement = clickedBtn.prev('.add-cart-loading');
	var nearestSuccessElement = clickedBtn.prevAll('.add-cart-success');
	var nearestFailElement = clickedBtn.prevAll('.add-cart-fail');

	if (response.Status && response.Status === 'Success') {
		clickedBtn.hide();
		nearestLoadingElement.hide();
		nearestSuccessElement.show();
		nearestSuccessElement.delay(1500).fadeOut(1000);

		//TODO: update the basket view

		appendNewBasketRow(response.Data);
		updateBasketView();

	} else if (response.Status && response.Status === 'Fail') {
		clickedBtn.hide();
		nearestLoadingElement.hide();
		nearestFailElement.show();
		nearestFailElement.delay(1500).fadeOut(1000);
	}
}

// ajaxActionLink remove functions:
function removeFromCartBegin(arg1, arg2, arg3) {
	console.log('begin');
	console.log(arg1);
	console.log(arg2);
	console.log(arg3);
}

function removeFromCartFail(arg1, arg2, arg3) {
	console.log('fail');
	console.log(arg1);
	console.log(arg2);
	console.log(arg3);
}

function removeFromCartSuccess(arg1, arg2, arg3) {
	console.log('success');
	console.log(arg1);
	console.log(arg2);
	console.log(arg3);
}