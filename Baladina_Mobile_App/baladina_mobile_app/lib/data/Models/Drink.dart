class Drink {
  late int id;
  late String drinkType;
  late String drinkName;
  late double drinkPrice;
  late int quantity;
  late String createDate;

  Drink.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    drinkType = json['drinkType'];
    drinkName = json['drinkName'];
    drinkPrice = json['drinkPrice'];
    quantity = json['quantity'];
    createDate = json['createDate'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    data['drinkType'] = this.drinkType;
    data['drinkName'] = this.drinkName;
    data['drinkPrice'] = this.drinkPrice;
    data['quantity'] = this.quantity;
    return data;
  }
}
