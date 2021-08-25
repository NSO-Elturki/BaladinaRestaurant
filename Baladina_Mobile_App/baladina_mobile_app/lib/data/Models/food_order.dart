class FoodOrder {
  late int id;
  late int orderId;
  late int foodId;

  FoodOrder(this.orderId, this.foodId);

  FoodOrder.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    orderId = json['orderId'];
    foodId = json['foodId'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['orderId'] = this.orderId;
    data['foodId'] = this.foodId;
    return data;
  }
}
