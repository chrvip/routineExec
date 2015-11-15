/**
 * Welcome to Pebble.js!
 *
 * This is where you write your app.
 */

var UI = require('ui');
var Vector2 = require('vector2');

var main = new UI.Card({
  title: 'Pebble.js',
  icon: 'images/menu_icon.png',
  subtitle: 'Hello World!',
  body: 'Press any button.',
  subtitleColor: 'indigo', // Named colors
  bodyColor: '#9a0036' // Hex colors
});
 
main.show();

main.on('click', 'up', function(e) {
	
    var ajax = require('ajax');
    console.log('a');
    ajax(
      {
        url: 'http://chrsoft.cn/data?act=lists&username=chr',
        type: 'json'
      },
      function(data, status, request) {
        //var json=JSON.Parse(data);
        var json=data;
        lists=data;
        var menu = new UI.Menu();
        for(var i=0;i<json.length;i++) {
            menu.item(0,menu.items(0).length,{title:json[i].name,subtitle:json[i].duration + 'min'});
        }
        
        
	//menu.on('select', onMenuSelect);
	
	  menu.on('select', function(e) {
	    console.log('Selected item #' + e.itemIndex + ' of section #' + e.sectionIndex);
	    console.log('The item is titled "' + e.item.title + '"');
	  });
		menu.show();
	      },
	      function(error, status, request) {
		console.log('The ajax request failed: ' + error);
	      }
	    );

});

main.on('click', 'select', function(e) {
  var wind = new UI.Window({
    fullscreen: true,
  });
  var textfield = new UI.Text({
    position: new Vector2(0, 65),
    size: new Vector2(144, 30),
    font: 'gothic-24-bold',
    text: 'Text Anywhere!',
    textAlign: 'center'
  });
  wind.add(textfield);
  wind.show();
});

main.on('click', 'down', function(e) {
  var card = new UI.Card();
  card.title('A Card');
  card.subtitle('Is a Window');
  card.body('The simplest window type in Pebble.js.');
  card.show();
});
