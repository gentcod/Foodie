import { useDispatch, useSelector } from "react-redux";
import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";
import L from 'leaflet';
import icon from 'leaflet/dist/images/marker-icon.png';
import iconShadow from 'leaflet/dist/images/marker-shadow.png';

import { Container, RestaurantLocation, RestaurantName } from "./map.style";
import { useEffect } from "react";
import { fetchRestaurantsStart } from "../../store/restaurant/restaurant.action";
import { selectRestaurants } from "../../store/restaurant/restaurant.selector";

let DefaultIcon = L.icon({
  iconUrl: icon,
  shadowUrl: iconShadow
});

L.Marker.prototype.options.icon = DefaultIcon;

const Map = () => {
  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(fetchRestaurantsStart());
  }, [dispatch]);

  const data = useSelector(selectRestaurants);

  return (
    <Container>
      <MapContainer
        center={[6.5244, 3.3792]}
        zoom={13}
        scrollWheelZoom={false}
        style={{height: '90%', width: '100%'}}>
      <TileLayer
        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />
      {data.map(restaurant => 
        <Marker key={restaurant.id} position={[restaurant.geolocation.latitude, restaurant.geolocation.longitude]}>
          <Popup>
            <RestaurantName>{restaurant.name}</RestaurantName>
            <RestaurantLocation>{restaurant.location}</RestaurantLocation>
          </Popup>
        </Marker>
      )}
    </MapContainer>
    </Container>
  );
};

export default Map;
