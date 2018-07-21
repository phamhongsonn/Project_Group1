
create database if not exists shopdb;
use shopdb;

create table users(
    user_name varchar(50) primary key,
    user_password varchar(50) not null
);

create table item(
    item_id int auto_increment primary key,
    item_name varchar(50),
    unit_price decimal(20,2),
    quantity decimal
);
create table orders(
    order_id int primary key auto_increment,
    user_name varchar(50),
    order_date datetime default current_timestamp
);

create table orderdetail(
    order_id int,
    item_id int,
    item_name varchar(50),
    price decimal(20,2),
    amount int not null default 1,
    constraint pk_orderdetail primary key(order_id,item_id),
    constraint fk_orderdetail_itemid foreign key(item_id) references item(item_id),
    constraint fk_orderdetail_orderid foreign key(order_id) references orders(order_id)
);

insert into users(user_name,user_password) values
('staff1','staff1');
insert into item(item_name,unit_price,quantity) VALUES
('dog T-shirt','100000','20'),
('cat T-shirt','150000','20'),
('bird T-shirt','300000','30'),
('cow T-shirt','400000','30'),
('jogger pant','450000','40'),
('short pant','150000','20'),
('flannel','300000','30'),
('black hoddies','400000','30'),
('gucci','450000','40'),
('caro T-shirt','150000','20'),
('Longtee','300000','30'),
('bird T-shirt','300000','30'),
('cow T-shirt','400000','30'),
('jogger pant','450000','40'),
('short pant','150000','20'),
('flannel','300000','30');
select * from orders;
select max(order_id)from orders;
insert into orders(order_id,user_name,order_date) values(1,'pham hong son','2018/01/01');