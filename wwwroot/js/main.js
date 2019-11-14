function handleToggle(index) {
    const handleList = ['seatArrangement', 'roomManagement', 'periodManagement', 'courseManagement'];
    const x = document.getElementById(handleList[index]);
    if (x.className.indexOf('nav-expand') === -1) {
        x.className += ' nav-expand';
    } else {
        x.className = x.className.replace(' nav-expand', '');
    }
}

