var vm = function(categories,fruits,vegetables){
var self = this;

self.categories = ko.observableArray(categories);
self.fruits = ko.observableArray(fruits);
self.vegetables = ko.observableArray(vegetables);

self.newfruit = ko.observable('new fruit');
self.newvegetable = ko.observable('new vegegable');
self.newcategory = ko.observable('new category');


//additions
self.addfruit = function(){
self.fruits.push(self.newfruit());

}

self.addvegetable = function(){
self.fruits.push(self.newvegetable());
}

self.addcategory = function(){
self.fruits.push(self.newcategory());
}

//removals
self.removefruit = function(){

self.fruits.remove(self);

}

self.removevegetable = function(){

self.vegetables.remove(self);

}

self.removecategory = function(){
self.categories.remove(self);

}


}





ko.applyBindings(new vm([],[],[]));