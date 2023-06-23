import datetime

# Клас для валідації даних
class Validator:
    @staticmethod
    def is_valid_number(value):
        try:
            float(value)
            return True
        except ValueError:
            return False

    @staticmethod
    def is_valid_string(value):
        if not isinstance(value, str):
            return False
        # Додайте інші перевірки на рядкові дані за потреби
        return True

# Клас продукту
class Product:
    def __init__(self, product_id, title, image_url, price, created_at, updated_at, description):
        self.product_id = product_id
        self.title = title
        self.image_url = image_url
        self.price = price
        self.created_at = created_at
        self.updated_at = updated_at
        self.description = description

    def __str__(self):
        return f"Product ID: {self.product_id}\nTitle: {self.title}\nPrice: {self.price}"

    def __repr__(self):
        return f"Product({self.product_id}, {self.title}, {self.image_url}, {self.price}, {self.created_at}, {self.updated_at}, {self.description})"

    # Методи доступу до полів класу
    def get_product_id(self):
        return self.product_id

    def get_title(self):
        return self.title

    def get_image_url(self):
        return self.image_url

    def get_price(self):
        return self.price

    def get_created_at(self):
        return self.created_at

    def get_updated_at(self):
        return self.updated_at

    def get_description(self):
        return self.description

    def set_product_id(self, product_id):
        self.product_id = product_id

    def set_title(self, title):
        self.title = title

    def set_image_url(self, image_url):
        self.image_url = image_url

    def set_price(self, price):
        self.price = price

    def set_created_at(self, created_at):
        self.created_at = created_at

    def set_updated_at(self, updated_at):
        self.updated_at = updated_at

    def set_description(self, description):
        self.description = description

# Клас колекції продуктів
class Collection:
    def __init__(self):
        self.products = []

    def add_product(self, product):
        self.products.append(product)

    def remove_product_by_id(self, product_id):
        for product in self.products:
            if product.get_product_id() == product_id:
                self.products.remove(product)
                return True
        return False

    def find_products(self, keyword):
        results = []
        for product in self.products:
            if keyword.lower() in product.get_title().lower() or keyword.lower() in product.get_description().lower():
                results.append(product)
        return results

    def sort_products(self, field):
        self.products.sort(key=lambda product: getattr(product, field))

    def __str__(self):
        return '\n'.join(str(product) for product in self.products)

# Читання з файлу
def read_products_from_file(filename):
    products = []
    with open(filename, 'r') as file:
        for line in file:
            data = line.strip().split(';')
            product_id = int(data[0])
            title = data[1]
            image_url = data[2]
            price = float(data[3])
            created_at = datetime.datetime.strptime(data[4], '%Y-%m-%d').date()
            updated_at = datetime.datetime.strptime(data[5], '%Y-%m-%d').date()
            description = data[6]
            product = Product(product_id, title, image_url, price, created_at, updated_at, description)
            products.append(product)
    return products

# Запис у файл
def write_products_to_file(filename, products):
    with open(filename, 'w') as file:
        for product in products:
            file.write(f"{product.get_product_id()};{product.get_title()};{product.get_image_url()};{product.get_price()};{product.get_created_at()};{product.get_updated_at()};{product.get_description()}\n")

# Тестування
def run_tests():
    # Тестування класу Product
    product = Product(1, "Phone", "phone.jpg", 999.99, datetime.date(2021, 1, 1), datetime.date(2021, 1, 1), "A smartphone")
    print(product)
    print(repr(product))

    # Тестування класу Collection
    collection = Collection()
    collection.add_product(product)
    print(collection)
    collection.remove_product_by_id(1)
    print(collection)

    # Тестування зчитування/запису з файлу
    products = read_products_from_file('products.txt')
    print(products)
    write_products_to_file('output.txt', products)

run_tests()