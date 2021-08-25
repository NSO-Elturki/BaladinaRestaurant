class DrinksOrder {
  late int id;
  late int orderId;
  late int drinkId;

  DrinksOrder(this.orderId, this.drinkId);

  DrinksOrder.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    orderId = json['orderId'];
    drinkId = json['drinkId'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['orderId'] = this.orderId;
    data['drinkId'] = this.drinkId;
    return data;
  }
}
