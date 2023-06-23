import random
import json
import datetime

# Генерування випадкових дат
def generate_random_date(start_date, end_date):
    time_between_dates = end_date - start_date
    days_between_dates = time_between_dates.days
    random_number_of_days = random.randrange(days_between_dates)
    random_date = start_date + datetime.timedelta(days=random_number_of_days)
    return random_date

# Генерування випадкових продуктів
def generate_random_products(num_products):
    products = []
    for i in range(num_products):
        product_id = i + 1
        title = f"Product {product_id}"
        image_url = f"image_{product_id}.jpg"
        price = round(random.uniform(10, 1000), 2)
        created_at = generate_random_date(datetime.date(2021, 1, 1), datetime.date(2022, 12, 31))
        updated_at = generate_random_date(created_at, datetime.date(2022, 12, 31))
        description = f"Description for Product {product_id}"
        product = Product(product_id, title, image_url, price, created_at, updated_at, description)
        products.append(product)
    return products

# Запис у JSON файл з випадковими продуктами
def write_random_products_to_file(filename, num_products):
    products = generate_random_products(num_products)
    write_products_to_file(filename, products)

# Записуємо 20 випадкових продуктів у файл 'random_products.json'
write_random_products_to_file('random_products.json', 20)