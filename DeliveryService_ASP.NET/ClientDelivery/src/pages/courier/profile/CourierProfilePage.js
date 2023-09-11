import useOrderService from "../../../services/OrderService";
import { useState, useEffect } from "react";

const CourierProfilePage = () => {

    const [data, setData] = useState(null);

    const {getCourierById} = useOrderService();

    useEffect(() => {
        getCourierById()
            .then(data => setData(data));
    }, []);

    console.log(data);

    const renderItems = (data) => {

        return(
            <div className="info">
            <h3 class="title-3">Email: {data.email}</h3>
            <h3 class="title-3">Пароль: {data.password}</h3>
            <h3 class="title-3">Фамилия: {data.lastName}</h3>
            <h3 class="title-3">Имя: {data.firstName}</h3>
            <h3 class="title-3">Дата рождения: {data.bitrhDay}</h3>
            <h3 class="title-3">Количество заказов: {data.countOrder}</h3>
            <h3 class="title-3">Номер телефона: {data.phoneNumber}</h3>
            <h3 class="title-3">Город: {data.city}</h3>
            </div>
        )   
    }

    let items;

    if(data !== null){
        items = renderItems(data);
    }


    return (
        <>
            {data !== null ? items : null}
        </>
        
    )
}

export default CourierProfilePage;