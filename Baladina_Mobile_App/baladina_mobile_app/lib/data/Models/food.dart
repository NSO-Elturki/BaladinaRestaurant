class Food {
  late int id;
  late String foodType;
  late String foodName;
  late double foodPrice;
  late String foodDescription;
  late String createDate;

  Food.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    foodType = json['foodType'];
    foodName = json['foodName'];
    foodPrice = json['foodPrice'];
    foodDescription = json['foodDescription'];
    createDate = json['createDate'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['foodType'] = this.foodType;
    data['foodName'] = this.foodName;
    data['foodPrice'] = this.foodPrice;
    data['foodDescription'] = this.foodDescription;
    return data;
  }
}
