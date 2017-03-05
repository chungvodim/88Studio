/*
 * Init env
 */
if(typeof window.params === 'undefined' || window.params === null){
    window.params = {};
}

window.params.barColors = ['#D30000', '#5F92CD', '#5BBA82', '#F9D021', '#9EA1AD'];

$.ajaxSetup({
    error: function (jqXHR, textStatus, errorThrown) {
        console.log(jqXHR.responseText);
        if (jqXHR.status == 401) {
            //window.location.href = window.ServerRoot + '/lg.aspx';
        } else {
            //alert("Error: " + textStatus + ": " + errorThrown);
        }
    }
});

String.prototype.contains = function (substr, ignoreCase) {
    var temp = this;
    if (ignoreCase) {
        var temp = temp.toLowerCase();
        substr = substr.toLowerCase();
    }
    return temp.indexOf(substr) > -1;
}

var _NOTY = null;

var _COMMON = {
    updateQueryStringParameter: function (uri, key, value) {
        var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
        var separator = uri.indexOf('?') !== -1 ? "&" : "?";
        if (uri.match(re)) {
            return uri.replace(re, '$1' + key + "=" + value + '$2');
        }
        else {
            return uri + separator + key + "=" + value;
        }
    },
    decodeHtml: function(str){
        return $("<div/>").html(str).text();
    },
    numberWithCommas: function (x) {
        return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    },

    toJsonString: function (obj) {
        return window.JSON ? JSON.stringify(obj) : $.toJSON(obj);
    },

    hexDigits: new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f"),

    //Function to convert hex format to a rgb color
    rgb2hex: function (rgb) {
        rgb = rgb.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);
        return "#" + this.hex(rgb[1]) + this.hex(rgb[2]) + this.hex(rgb[3]);
    },

    hex: function (x) {
        return isNaN(x) ? "00" : this.hexDigits[(x - x % 16) / 16] + this.hexDigits[x % 16];
    },

    noty: function(type, text, timeout) {
        if (_NOTY != null) {
            _NOTY.closeCleanUp();
        }

        var noTimeOut = typeof timeout === 'undefined' || timeout == null;

        _NOTY = noty({
            text: text,
            type: type, // "alert", "success", "error", "warning", "information", "confirm"
            layout: 'topCenter',
            theme: 'bootstrapTheme',
            killer: false,
            timeout: noTimeOut ? false : timeout,
            animation: {
                open: 'animated fadeInDown', // flipInX
                close: 'animated fadeOutUp' // flipOutX                                   
            }
        });
    }
}