import Cart from "../../components/cart/Cart";

const CartPage = ({cartItems, setCartItems}) => {
    return(
        <div>
            <Cart cartItems={cartItems} setCartItems={setCartItems}/>
        </div>
    )
}

export default CartPage;