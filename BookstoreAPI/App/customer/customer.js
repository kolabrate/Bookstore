
var vm = function(names){
var self = this;


self.name = ko.observable("");
self.names = ko.observableArray(names);
self.addname = function(){
	self.names.push(self.name());
	self.name("");
};
self.removename = function(){
	self.names.pop(self);//remove doesnt work for some reason.
};

	



}



ko.applyBindings(new vm(['Anand','Maran']));