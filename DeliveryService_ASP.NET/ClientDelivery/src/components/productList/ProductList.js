import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import useOrderService from "../../services/OrderService";
import { Button } from "@mui/material";
import './productList.scss';

const ProductList = ({cartItems, setCartItems}) => {

    const [dataProducts, setDataProducts] = useState(null);
    const [dataSections, setDataSections] = useState(null);
    
    const navigate = useNavigate();

    const {getAllProducts, getAllSections, getCities} = useOrderService();

    useEffect(() => {
        getAllProducts()
            .then(dataProducts => setDataProducts(dataProducts));
    }, []);

    useEffect(() => {
        getAllSections()
            .then(dataSections => setDataSections(dataSections));
    }, []);

    const addCart = (item) => {
        setCartItems(cartItems => [...cartItems, item])
        return cartItems;
    }

    const navigateSection = (sectionId) => {
        navigate(`/product/${sectionId}`);
    }

    const renderSections = (data) => {
        const sections = data.sections.map(item => {
            return(
                <div className="button__input">
                    <Button variant="contained" size="medium" type="submit"                       
                        onClick={() => navigateSection(item.sectionId)}>{item.name}
                    </Button>
                </div>   
            )
        })

        return sections;
    }

    const renderItems = (data) => {
        
        const items = data.products.map((item, i) => {

            return(
                <>
                <li className="products__item" key={i}>
                    
                    <img src={'./img/' + item.thumbnail} alt={item.title} className="products__item-img"/>
                    <div className="products__item-name">{item.title} </div>
                    <div className="products__item-price">{item.price + "₽"}</div>
                    <Button onClick={() => addCart(item)} disabled={cartItems.some(o => o.productId === item.productId)}>В корзину</Button>
                </li>
                </>
            )
        }) 

        return (
            <ul className="products__grid">
                {items}
            </ul>
        )
    }

    let sections;
    if(dataSections !== null){
        sections = renderSections(dataSections);
    }

    let items;
    if(dataProducts !== null){
        items = renderItems(dataProducts);
    }

    console.log(cartItems);
    
    return (
        <div className="products__list">
            {sections}
            {items}
        </div>
    )
}

export default ProductList;