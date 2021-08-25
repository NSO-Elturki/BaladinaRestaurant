class OrderBill {
  late int id;
  late int orderId;
  late String clientName;
  late int clientPhoneNumber;
  late String clientAddress;
  late String email;
  late String city;
  late String note;
  late double totalCost;

  OrderBill(this.orderId, this.clientName, this.clientPhoneNumber,
      this.clientAddress, this.email, this.city, this.note, this.totalCost);

  OrderBill.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    orderId = json['orderId'];
    clientName = json['clientName'];
    clientPhoneNumber = json['clientPhoneNumber'];
    clientAddress = json['clientAddress'];
    email = json['email'];
    city = json['city'];
    note = json['note'];
    totalCost = json['totalCost'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['orderId'] = this.orderId;
    data['clientName'] = this.clientName;
    data['clientPhoneNumber'] = this.clientPhoneNumber;
    data['clientAddress'] = this.clientAddress;
    data['email'] = this.email;
    data['city'] = this.city;
    data['note'] = this.note;
    data['totalCost'] = this.totalCost;
    return data;
  }
}
