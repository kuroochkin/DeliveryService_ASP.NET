import React, { useState } from 'react';
import Cards from 'react-credit-cards-2';
import useOrderService from '../../services/OrderService';
import { Button } from '@mui/material';
import 'react-credit-cards-2/dist/lib/styles.scss';
import './paymentForm.scss';

const PaymentForm = ({cartItems, setCartItems}) => {
  const [state, setState] = useState({
    number: '',
    expiry: '',
    cvc: '',
    name: '',
    focus: '',
  });

  const {createOrder} = useOrderService();

  const handleInputChange = (evt) => {
    const { name, value } = evt.target;
    
    setState((prev) => ({ ...prev, [name]: value }));
  }

  const handleInputFocus = (evt) => {
    setState((prev) => ({ ...prev, focus: evt.target.name }));
  }

  const handleSubmit = async e => {
    e.preventDefault();

    console.log(cartItems);

    
    const data1 = {
        description: '',
        products: [    
        ]
    };

    for (let i = 0; i < cartItems.length; i++) {
        data1.products[i] = {};
        cartItems[i].priceTotal = cartItems[i].priceTotal.replace(/\./g, ',');
        data1.products[i].productId = cartItems[i].productId;
        data1.products[i].totalPrice = cartItems[i].priceTotal;
        data1.products[i].count = String(cartItems[i].count);
        data1.products[i].thumbnail = cartItems[i].thumbnail;
        data1.products[i].title = cartItems[i].title;
    }

    const data = await createOrder(data1);

    console.log(data);

    if (data?.status === 500){
        e.target.reset(); 
    }
    else{
        console.log('Все успешно!');
    }

    setCartItems(cartItems => cartItems = []);
}

  return (
    <div className='container__2'>
            <div>
        <Cards
            number={state.number}
            expiry={state.expiry}
            cvc={state.cvc}
            name={state.name}
            focused={state.focus}
        />
        <form>
            <input type="text" name="number" placeholder="Номер карты"value={state.number} onChange={handleInputChange}
            onFocus={handleInputFocus}
            />
        </form>
        <form>
            <input type="text" name="expiry" placeholder="Срок истечения"value={state.expiry} onChange={handleInputChange}
            onFocus={handleInputFocus}
            />
        </form>
        <form>
            <input type="text" name="cvc" placeholder="CVC"value={state.cvc} onChange={handleInputChange}
            onFocus={handleInputFocus}
            />
        </form>
        <form>
            <input type="text" name="name" placeholder="Имя владельца"value={state.name} onChange={handleInputChange}
            onFocus={handleInputFocus}
            />
        </form>

        <form onSubmit={handleSubmit}> 

            <div className="button input">
                <Button variant="contained" size="medium" type="submit">Оплатить!</Button>
            </div>     
        </form>

        </div>
    </div>
   
  );
}

export default PaymentForm;