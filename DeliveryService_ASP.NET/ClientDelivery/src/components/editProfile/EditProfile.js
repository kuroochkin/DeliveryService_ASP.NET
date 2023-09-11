import { useState, useEffect} from "react";
import { Button, Checkbox} from "@mui/material";
import useOrderService from "../../services/OrderService";
import "./editProfile.scss";

const EditProfile = ({setToken}) => {

    const [data, setData] = useState(null);

    const [email, setEmail] = useState();
    const [password, setPassword] = useState();
    const [lastName, setLastName] = useState();
    const [firstName, setFirstName] = useState();
    const [phoneNumber, setPhoneNumber] = useState();
    const [city, setCity] = useState();

    const [itsOk, setItsOk] = useState(false);
    const [isRequest, setIsRequest] = useState(false);

    const [typePass, setTypePass] = useState("password");

    const {getCustomerById, editCustomerProfile} = useOrderService();

    useEffect(() => {
        getCustomerById()
            .then(data => setData(data));
    }, []);

    console.log(data);

    // let countOrder = data.countOrder;
    // let customerId = data.id;
    // let birthday = data.birthDay;
    
    const handleSubmit = async e => {
        e.preventDefault();

        setIsRequest(true);

        const data1 = await editCustomerProfile({
            // customerId,
            // birthday,
            // city,
            // countOrder,
            email,
            password,
            firstName,
            lastName,
            phoneNumber
        });

        console.log(data1);

        if (data1?.status === 500){
			console.log('Очистка формы')
			e.target.reset(); 
		}
        else {
            setToken(data.token);
            setItsOk(true);
        }

        setIsRequest(false);
    };

    const seePass = () => {
        if(typePass === "text"){
            setTypePass("password");
        } else {
            setTypePass("text");
        }
    };

    const renderItems = (data) => {
        return (
            <div className="cont">
                <form onSubmit={handleSubmit}> 
                    <div className="text input">
                        <label>
                            <p>Имя</p>
                            <input type="text" placeholder={data.firstName} onChange={e => setFirstName(e.target.value)}/>
                        </label>
                    </div>
                    <div className="text input">
                        <label>
                            <p>Фамилия</p>
                            <input type="text" placeholder={data.lastName} onChange={e => setLastName(e.target.value)}/>
                        </label>
                    </div>
                    <div className="email input">
                        <label>
                            <p>Почта</p>
                            <input type="text" placeholder={data.email} onChange={e => setEmail(e.target.value)}/>
                        </label>
                    </div>
                    <div className="password input">
                        <label>
                            <p>Пароль</p>
                            <input type={typePass} disabled={true} value={data.password} onChange={e => setPassword(e.target.value)}/>
                        </label>
                        
                    </div>
                    <Checkbox onChange={seePass}/>
                    <div className="text input">
                        <label>
                            <p>Номер телефона</p>
                            <input type="text" disabled={true} value={data.phoneNumber} onChange={e => setPhoneNumber(e.target.value)}/>
                        </label>
                    </div>
                    <div className="text input">
                        <label>
                            <p>Город</p>
                            <input type="text" value={data.city} onChange={e => setCity(e.target.value)}/>
                        </label>
                    </div>
                    <div className="button input">
                        <Button variant="contained" size="medium" type="submit">Далее</Button>
                    </div>   
                </form>
            </div>
        )
    }
    let items;

    if(data !== null){
        items = renderItems(data);
    }

    return (
        <div className='cont'>
            {data !== null ? items : null}
        </div> 
    )
}

export default EditProfile;