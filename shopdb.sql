drop database shopdb;
create database if not exists shopdb;
use shopdb;

create table users(
    user_name varchar(50) primary key,
    user_password varchar(50) not null
);

create table item(
    item_id int auto_increment primary key,
    item_name varchar(50),
    unit_price decimal(20,0),
    quantity int
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
    price decimal(20,0),
    amount int not null default 1,
    constraint pk_orderdetail primary key(order_id,item_id),
    constraint fk_orderdetail_itemid foreign key(item_id) references item(item_id),
    constraint fk_orderdetail_orderid foreign key(order_id) references orders(order_id)
);
delimiter $$
create trigger tg_before_insert before insert
on item for each row
    begin
		if new.quantity < 0 then
            signal sqlstate '45001' set message_text = 'Not valid quantity';
	end if;
end $$
delimiter ;

delimiter $$
create trigger tg_CheckAmount
before update on item
for each row
	begin
		if new.quantity < 0 then
            signal sqlstate '45001' set message_text = 'Not enough item';
	end if;
end $$
delimiter ;
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
