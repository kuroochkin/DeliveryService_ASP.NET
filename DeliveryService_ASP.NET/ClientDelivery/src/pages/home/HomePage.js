import ProductList from "../../components/productList/ProductList";
import { Button } from "@mui/material";


const HomePage = ({cartItems, setCartItems}) => {

    return (  
        <div>
            <ProductList cartItems={cartItems} setCartItems={setCartItems}/>
        </div>  
    )
}

export default HomePage;