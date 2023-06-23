from product import Product
import json
import datetime

class Collection:
    def __init__(self, products = []):
        self.products = products
    
    def __add__(self, product):
        self.products.append(product)

    def __sub__(self, product_id):
        todel = 0
        for i in range(len(self.products)):
            if self.products[i].product_id == product_id:
                todel = i
        del self.products[todel]

    def find_product(self, keyword):
        results = []
        for product in self.products:
            if keyword.lower() in product.__str__().lower():
                results.append(product)
                break
        c = Collection(results)
        return c
    
    def sort(self, key):
        self.products.sort(key = lambda product: getattr(product, key))

    
    def __str__(self):
        return '\n'.join(str(product) for product in self.products)
    
    def getlen(self):
        return len(self.products)
    
    def from_json(filename):
        products = Collection()
        with open(filename, 'r') as f:
            data = json.load(f)
            for i in data:
                products + Product.from_dict(i)
        return products
    
    def to_json(self, filename):
        data = []
        for product in self.products:
            item = product.to_dict()
            data.append(item)

        with open(filename, 'w') as file:
            json.dump(data, file, indent=4)

    def get_first(self):
        return self.products[0]
    