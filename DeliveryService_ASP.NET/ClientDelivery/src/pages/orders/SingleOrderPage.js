import {useParams, Link} from 'react-router-dom';
import { useState, useEffect } from "react";
import useOrderService from '../../services/OrderService';
import './orderSinglePage.scss';



const SingleOrderPage = () => {
    const {orderId} = useParams();

    const [data, setData] = useState(null);

    const {getOrderById} = useOrderService();

    useEffect(() => {
        getOrderById(orderId)
            .then(data => setData(data))
    }, [orderId]);

    console.log(data);

    let count = 0;
    const totalCount = () => {
        data.products.map(product => {
            count += (+product.count);
        })
        return count;
    }

    let total = 0
    const totalPrice = () => {
        data.products.map(product => {
            product.totalPrice = parseFloat(product.totalPrice.replace(/\,/g, '.'));
            total += (product.totalPrice);
        })
        return total;
    }
    
    const renderItems = (data) => {

        return (
            <>
            <section class="section-cart1">
                <h3 class="title-3">Дата и время создания заказа: {data.created.slice(0,10) + ' '+ data.created.slice(11,16)}</h3>
                <h3 class="title-3">Дата и время получения заказа: {data.status === 'Complete' ?  (data.end.slice(0,10) + ' '+ data.end.slice(11,16)) : '...'}</h3>
                <h3 class="title-3">Описание: {data.description}</h3>
                <h3 class="title-3">Статус: {data.status === 'Create' ? 'Обрабатываем...' : (data.status === 'Progress' ? 'Курьер в пути!' : 'Заказ доставлен!') }</h3>
                <h3 class="title-3">Курьер: {data.courier.lastName ? (data.courier.firstName + ' ' + data.courier.lastName) : 'Подбираем курьера' }</h3>
                <header class="section-cart__header">
                    <div class="container2">
                        <h3 class="title-2">Товары из заказа</h3>
                    </div>
                </header>

                <div class="section-cart__body">
                    <div class="container2">

                <section class="cart">
                    <header class="cart-header">
                        <div class="cart-header__title">наименование</div>
                        <div class="cart-header__count">количество</div>
                        <div class="cart-header__cost">стоимость</div>
                    </header>
                    

                    {data.products.map((product) => (
                        
                        <section class="product">
                            <div class="product__img1"><img src={'../img/' + product.thumbnail} alt="Apple MacBook Air 12"/></div>
                                <div class="product__title">{product.title}</div>
                                    <div class="product__count">
                                        <div class="count1">
                                            <input type="number" class="count__input" value={product.count}/>                            
                                        </div>
                                    </div>
                                <div class="product__price">{product.totalPrice + `₽`}</div>
                                <div class="product__controls">
                        </div>
                        </section>
                    ))}

                    <footer class="cart-footer">
                        <div class="cart-footer__count">{totalCount()}</div>
                        <div class="cart-footer__price">{totalPrice() + `₽`}</div>
                    </footer>
                </section>
            </div>
        </div>
    </section>
    </>
        )
    }

    let items;
    if(data !== null){
        items = renderItems(data);
    }

    return (
        <div className='orderlist1_container'>
            {data !== null ? items : null}
        </div>
        
    )

}

export default SingleOrderPage;