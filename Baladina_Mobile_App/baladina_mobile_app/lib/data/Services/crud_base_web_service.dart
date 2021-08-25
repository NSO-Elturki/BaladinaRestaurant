import 'package:baladina_website/constants/strings.dart';
import 'package:dio/dio.dart';

abstract class CrudBaseWebService {
  late Dio dio;
  late String endPoint;

  CrudBaseWebService() {
    BaseOptions options = BaseOptions(
      baseUrl: baseUrl,
      receiveDataWhenStatusError: true,
      connectTimeout: 10 * 1000,
      receiveTimeout: 10 * 1000,
    );

    dio = new Dio(options);
  }

  Future<List<dynamic>> getAll() async {
    try {
      Response response = await dio.get(endPoint);
      print(response.data.toString());
      return response.data;
    } catch (e) {
      print('ERROOOOOOOR ${e.toString()}');
      return [];
    }
  }
}
