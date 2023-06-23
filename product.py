import json
import datetime
from validator import Validator

class Product:

    def __init__(self, product_id, title, image_url, price, created_at, updated_at, description):
        self.product_id= product_id
        self.title = title
        self.image_url = image_url
        self.price= price
        self.created_at = created_at
        self.updated_at = updated_at
        self.description = description
        Validator.validate_date(self.created_at, self.product_id)
        Validator.validate_date(self.updated_at, self.product_id)
        Validator.validate_name(self.title, self.product_id)
        Validator.validate_price(self.price, self.product_id)

    def __str__(self):
        return f"Product ID: {self.product_id}\nTitle: {self.title} \nImage_url: {self.image_url}\nPrice: {self.price}\nCreated_at: {self.created_at}\nUpdated_at: {self.updated_at}\nDescription: {self.description}\n\n"

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

    def set_url(self, image_url):
        self.image_url = image_url

    def set_price(self, price):
        self.price = price
    
    def set_created_at(self, created_at):
        self.created_at = created_at

    def set_updated_at(self, updated_at):
        self.updated_at = updated_at
    
    def set_description(self, description):
        self.description = description
    
    def from_dict(dict):
        return Product(int(dict['product_id']), dict['title'], dict['image_url'], 
                       float(dict['price']), 
                       datetime.datetime.strptime(dict['created_at'], '%d-%m-%Y'), 
                       datetime.datetime.strptime(dict['updated_at'], '%d-%m-%Y'),
                         dict['description'])
    
    def to_dict(self):
        item = {
            'product_id': self.get_product_id(),
            'title': self.get_title(),
            'image_url': self.get_image_url(),
            'price': self.get_price(),
            'created_at': self.get_created_at().strftime('%d-%m-%Y'),
            'updated_at': self.get_updated_at().strftime('%d-%m-%Y'),
            'description': self.get_description()
        }
        return item

