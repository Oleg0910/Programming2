import json
import datetime

class Validator:
    def validate_date(value, id):
        if type(value) == str: 
            try:
                date = datetime.datetime.strptime(value, '%d-%m-%Y')
            except ValueError:
                raise Exception("помилка в даті:", id)
        else:
            date = value
        if date > datetime.datetime.now():
            raise Exception("помилка в даті:", id)
        
    def validate_name(value, id):
        whitelist = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0987654321 '
        for v in value:
            if v not in whitelist:
                raise Exception("помилка в назві:", id)
            
    def validate_price(value, id):
        try:
            price = float(value)
        except ValueError:
            raise Exception("помилка в ціні:", id)
        if price < 0:
            raise Exception("помилка в ціні:", id)


    
   
         