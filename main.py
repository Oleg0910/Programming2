import json
import datetime
from product import Product
from validator import Validator
from collection import Collection

def read_from_file(file_name):
    products = Collection.from_json(file_name)
    return products

def write_to_file(filename):
    data = read_from_file(filename)
    id = int(input("введіть id товару :"))
    name = input("введіть назву товару:")
    link = input("введіть посилання на картинку:")
    price = float(input("введіть ціну товару:"))
    created = datetime.datetime.now()
    updated = datetime.datetime.now()
    description = input("введіть опис товару:")
    
    product = Product(id, name, link, price, created, updated, description)
    data + product
    data.to_json(filename)

def validate_file(filename):
    with open('products.json', 'r') as f:
        data = json.load(f)
    for i in data:
        Validator.validate_date(i['created_at'], i['product_id'])
        Validator.validate_date(i['updated_at'], i['product_id'])
        Validator.validate_name(i['title'], i['product_id'])
        Validator.validate_price(i['price'], i['product_id'])

def delete_from_file(filename):
    id = int(input("введіть id товару, який бажаєте видалити(осточортів вам):"))
    data = read_from_file(filename)
    data - id
    data.to_json(filename)

def finding_now_product(filename):
    keyword = input("введіть слово по якому будете шукати:")
    data = read_from_file(filename)
    results = data.find_product(keyword)
    print(results)

def sorting_now_products(filename):
    data = read_from_file(filename)
    while True:
        try:
            n = input(f"оберіть атрибут по якому сортувати з переліку:{data.get_first().to_dict().keys()}")
            break
        except ValueError:
            print("виберіть пункт з переліку!")
            continue
    data.sort(n)
    print(data)
    print('відсортовано, ваша величність')

if __name__ == "__main__":
    file_name = input('введіть назву файлу:')
    while True:
        print("""
        1 - валідація
        2 - вивести на екран 
        3 - ввести новий товар
        4 - видалити товар по id
        5 - пошук товару
        6 - сортування
        0 - вийти
        """)
        try:
            n = int(input("оберіть пункт:"))
        except ValueError:
            print("виберіть пункт з переліку!")
            continue
        try:
            if n == 0:
                exit()
            if n == 1:
                validate_file(file_name)
            if n == 2:
                products = read_from_file(file_name)
                print(f"завантажено {products.getlen()} товарів")
                print(products)
            if n == 3:
                write_to_file(file_name)
                print("додано новий товар! :)")
            if n == 4:
                delete_from_file(file_name)
                print("товар успішно видалено!(сподіваємось не мозолить вам більше очі)")
            if n == 5:
                finding_now_product(file_name)
            if n == 6:
                sorting_now_products(file_name)
        except ValueError:
            print("ви ввели неправильні значення")
            continue