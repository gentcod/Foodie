import { MapContainer, Marker, Popup, TileLayer } from "react-leaflet";

import { Container } from "./map.style";

const Map = () => {

  return (
    <Container>
      <MapContainer
        center={[7.287995329999998, 5.1475001939999998]}
        zoom={13}
        scrollWheelZoom={false}
        style={{height: '35rem', width: '100%'}}>
      <TileLayer
        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />
      <Marker position={[7.287995329999998, 5.1475001939999998]}>
        <Popup>
          A pretty CSS3 popup. <br /> Easily customizable.
        </Popup>
      </Marker>
    </MapContainer>
    </Container>
  );
};

export default Map;
