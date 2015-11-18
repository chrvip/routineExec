/**
 * Welcome to Pebble.js!
 *
 * This is where you write your app.
 */
 //default value for test
var DefaultLists = [
            {
                "Items": [
                  { "id": 1, "listId": 1, "name": "dress up", "duration": 5, "showOrder": 1 },
                  { "id": 2, "listId": 1, "name": "morning question", "duration": 3, "showOrder": 2 },
                  { "id": 3, "listId": 1, "name": "shoes", "duration": 3, "showOrder": 3 }
                ], "id": 1, "name": "morning", "showOrder": 1, "duration": 11
            },
            {
                "Items": [
                  { "id": 4, "listId": 2, "name": "lunch", "duration": 30, "showOrder": 1 },
                  { "id": 5, "listId": 2, "name": "chat", "duration": 15, "showOrder": 2 },
                  { "id": 6, "listId": 2, "name": "rest", "duration": 15, "showOrder": 3 },
                  { "id": 7, "listId": 2, "name": "coffee", "duration": 5, "showOrder": 4 }
                ], "id": 2, "name": "noon", "showOrder": 2, "duration": 65
            },
            {
                "Items": [
                  { "id": 8, "listId": 3, "name": "supper", "duration": 20, "showOrder": 1 },
                  { "id": 9, "listId": 3, "name": "chat", "duration": 15, "showOrder": 2 },
                  { "id": 10, "listId": 3, "name": "son", "duration": 30, "showOrder": 3 }
                ], "id": 3, "name": "evening", "showOrder": 3, "duration": 65
            },
            {
                "Items": [
                  { "id": 11, "listId": 4, "name": "wash", "duration": 3.5, "showOrder": 1 },
                  { "id": 12, "listId": 4, "name": "read", "duration": 60, "showOrder": 2 }
                ], "id": 4, "name": "night", "showOrder": 4, "duration": 63.5
            }];
//fetch values stored in localStorage
var Settings = require('settings');
var lists=Settings.option("lists");
if(undefined==lists) {
	console.log('use default lists');
	lists=DefaultLists;
	Settings.option("lists",JSON.stringify(lists));
} else {
	console.log('parse  lists string to lists json obj');
	lists=JSON.parse(lists);
}
var Url="http://192.168.202.1/cfg.htm";
Url='http://www.chrsoft.cn/cfg.htm';
Url  += '?at='+Pebble.getAccountToken() ;//+ "&lists=" + JSON.stringify(lists) 

Settings.config(// JSON.stringify(lists)
	{ url: Url},
	function(e) {
		console.log('opening configurable');

		// Reset color to red before opening the webview
		//Settings.option('color', 'red');
	},
	function(e) {
		console.log('closed configurable');
		
		// Show the parsed response
		console.log(JSON.stringify(e.options));

		// Show the raw response if parsing failed
		if (e.failed) {
			console.log(e.response);
		}
		/*
		var data=e.response;
		console.log(data);
		console.log('');
		console.log('');
		console.log('');
		data= decodeURIComponent(data);
		console.log(data);
		//save data
		Settings.option("lists",data);
		*/
	}
);

var UI = require('ui');
var Vector2 = require('vector2');

var main = new UI.Card({
  title: 'Pebble.js',
  icon: 'images/menu_icon.png',
  subtitle: 'Hello World!',
  body: 'Press any button.',
  subtitleColor: 'indigo', // Named colors
  bodyColor: 'black' // Hex colors
});
 
main.show();
function getTimeStr(seconds) {
	var min=Math.floor(seconds / 60);
	var sec=seconds % 60;
	var result=min.toString(10) + ":";
	if (sec<10) {
		result +="0";
	}
	result +=sec.toString(10);
	return result;
}
var aList, listData,listIndex, listName;
var seconds, listItemIndex, textTitle, textTime, myTimeout ;
function onItemWindowDown(e)  {
	clearInterval (myTimeout);
	listItemIndex++;
	if(listItemIndex>=listData.length) {
		textTitle.text('Good Job!');
		textTime.text('');
		return;		
	}
	textTitle.text(listData[listItemIndex].name);
	seconds= 60 * listData[listItemIndex].duration;
	
	myTimeout = setInterval(
		function(){
			seconds--;
			textTime.text(getTimeStr(seconds));
		},
		1000 // timeout in ms 
	); 
}

function onMenuSelect(e) {
	listIndex=e.itemIndex;
	console.log('Selected item #' + listIndex + ' of section #' + e.sectionIndex);
	console.log('The item1 is titled "' + e.item.title + '"');
	aList=lists[listIndex];
	console.log('The item id in db is "' + aList.id + '"');
	
	listData=aList.Items;
	console.log('listData.length:' + listData.length);
	var text="";
	for (var i=0;i<listData.length;i++) {
		text += listData[i].name + ": " + listData[i].duration + "min\n"; 
	}
	console.log(text);
	
	var card = new UI.Card();
	card.title(listIndex.toString(10) +'. ' + aList.name);
	card.subtitle(aList.duration + "min");
	card.body(text);
	
	card.on('click', 'select', function(e) {
		var wind = new UI.Window({
			fullscreen: true,
		});
		textTitle = new UI.Text({
			position: new Vector2(0, 30),
			size: new Vector2(144, 30),
			font: 'gothic-24-bold',
			text: listData[0].name,
			textAlign: 'center'
		});
		wind.add(textTitle);
		listItemIndex=0;
		seconds= 60 * listData[listItemIndex].duration;
		textTime = new UI.Text({
			position: new Vector2(0, 70),
			size: new Vector2(144, 30),
			font: 'gothic-24-bold',
			text: getTimeStr(seconds),
			textAlign: 'center'
		});
		wind.add(textTime);
		wind.show();
		wind.on('click','down',onItemWindowDown);
		myTimeout = setInterval(
			function(){
				seconds--;
				textTime.text(getTimeStr(seconds));
			},
			1000 // timeout in ms 
		); 
	});
	card.show();	
}
main.on('click', 'up', function(e) {
	console.log('main click up');
	var menu = new UI.Menu();
	for(var i=0;i<lists.length;i++) {
		menu.item(0,menu.items(0).length,{title:lists[i].name,subtitle:lists[i].duration + 'min'});
	}
	menu.on('select', onMenuSelect);
	menu.show();
});

main.on('click', 'select', function(e) {
  var wind = new UI.Window({
    fullscreen: true,
  });
  var textfield = new UI.Text({
    position: new Vector2(0, 15),
    size: new Vector2(144, 90),
    font: 'gothic-24-bold',
    text: 'AccountToken:\n' + Pebble.getAccountToken(),
    textAlign: 'center'
  });
  wind.add(textfield);
  wind.show();
});

main.on('click', 'down', function(e) {
	
        console.log("Pebble Account Token: "+Pebble.getAccountToken());
        console.log("lists: "+lists);
	
  var card = new UI.Card();
  card.title('A Card');
  card.subtitle('Is a Window');
  card.body('The simplest window type in Pebble.js.');
  card.show();
});
/*
Pebble.addEventListener('showConfiguration', function(e) {
  // Show config page
  Pebble.openURL('http://www.chrsoft.cn/cfg?at='+Pebble.getAccountToken());
});
*/

