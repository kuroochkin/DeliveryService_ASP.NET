import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './cart.scss';
import { Button } from '@mui/material';
import {
    FaAngleUp,
    FaAngleDown,
    FaRegTrashAlt}
    from 'react-icons/fa';

const Cart = ({cartItems, setCartItems}) => {

    const [description, setDescription] = useState();

    const navigate = useNavigate();

    cartItems.map(product => (+product.priceTotal));
    cartItems.map(product => (+product.count));

    cartItems.map(product => {
        if(product.priceTotal === undefined)
        {
            product.priceTotal = parseFloat(product.price.replace(/\,/g, '.'));
        } else { 
            product.priceTotal = (product.count * parseFloat(product.price.replace(/\,/g, '.'))).toFixed(2);
        }
    })

    cartItems.map(product => {
        if(product.count === undefined)
        {
            product.count = 1;
        }
    })

    let totalSum = 0;
    let totalProducts = 0;
    cartItems.map(product => {
        totalSum = (totalSum + parseFloat(product.priceTotal));
        totalProducts = (totalProducts + Number(product.count))
    })
    totalSum = totalSum.toFixed(2);
   

    const increase = (id) => {
        setCartItems(cartItems => (
            cartItems.map(product => {
                if(product.productId === id) {
                    return {
                        ...product,
                        count: product.count + 1,
                        priceTotal: (product.count + 1) * product.price,
                    };
                }
                return product;
            })
        ))
    }

    const decrease = (id) => {
        setCartItems(cartItems => (
            cartItems.map(product => {
                if(product.productId === id) {
                    if(product.count == 1 || product.count == 0){
                        deleteProduct(id);
                    }
                    return {
                        ...product,
                        count: product.count - 1,
                        priceTotal: (product.count - 1) * product.price,
                    };
                }

                return product;
            })
        ))
    }

    console.log(cartItems);

    const deleteProduct = (id) => {
		setCartItems((cartItems) => cartItems.filter((product)=> id !== product.productId));
	}

    const changeValue = (id) => {
        cartItems.forEach(product => {
            if(product.productId === id) {
                return product.count;
            }
        })
    }

    const handleSubmit = () => {
        navigate(`/payment`);
    }

    const renderItems = (data) => {

        return (
            <section class="section-cart">
                <header class="section-cart__header">
                    <div class="container1">
                        <h3 class="title-2">Корзина товаров</h3>
                    </div>
                </header>

                <div class="section-cart__body">
                    <div class="container1">

                <section class="cart">
                    <header class="cart-header">
                        <div class="cart-header__title">наименование</div>
                        <div class="cart-header__count">количество</div>
                        <div class="cart-header__cost">стоимость</div>
                    </header>

                    {data.map((product) => (
                        <section class="product">
                            <div class="product__img"><img src={'./img/' + product.thumbnail} alt="Apple MacBook Air 13"/></div>
                                <div class="product__title">{product.title}</div>
                                    <div class="product__count">
                                        <div class="count">
                                            <div class="count__box">
                                                <input type="number" onChange={()=>{changeValue(product.productId)}}class="count__input" min="1" max="100" value={product.count}/>
                                            </div>
                                            <div class="count__controls">
                                                <button type="button" onClick={() => increase(product.productId)} class="count__up">
                                                    <div className="icon">{<FaAngleUp/>}</div>
                                                </button>
                                                <button type="button" onClick={() => decrease(product.productId)} class="count__down">
                                                    <div className="icon">{<FaAngleDown/>}</div>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                <div class="product__price">{product.priceTotal + `₽`}</div>
                                <div class="product__controls">
                                <button type="button" onClick={() => deleteProduct(product.productId)}>
                                    <div className="icon">{<FaRegTrashAlt/>}</div>
                                </button>
                        </div>
                        </section>
                    ))}
                    

                
                    <footer class="cart-footer">
                        <div class="cart-footer__count">{totalProducts}</div>
                        <div class="cart-footer__price">{totalSum + `₽`}</div>
                    </footer>
                </section>

                <form onSubmit={handleSubmit}> 
                    <div className="input1">
                        <label>
                            <input type="text" style={{"width": 700}} placeholder='Описание' onChange={e => setDescription(e.target.value)}/>
                        </label>
                    </div>
                    <div className="button input">
                        <Button variant="contained" size="medium" type="submit">Заказать!</Button>
                    </div>     
                </form>

            </div>
        </div>
    </section>


        )
    }

    let items;
    if(cartItems.length > 0){
        items = renderItems(cartItems);
    } else {
        return (
            <div class="container1">
                <h1 class="title-1">Корзина пуста</h1>
            </div>
        )
    }

    return(
        <>
        {items}
        </>
    )

}  

export default Cart;