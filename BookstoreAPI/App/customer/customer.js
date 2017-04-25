var vm = function(names){
var self = this; //use this in KO for managing scope hassle.

self._names = ko.observableArray(names);
self._newval = ko.observable(null);

self._addnewval = function(){
	console.log(self._newval());
	if(self._newval() != null){

			console.log('inside function');
			self._names.push(self._newval());
			self._newval(null);

	}
};

	

};

ko.applyBindings(new vm(['Anand','Maran','Priya']));