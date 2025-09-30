// wwwroot/swagger-custom.js
document.addEventListener('DOMContentLoaded', () => {
    // Tạo container cho search box
    const searchContainer = document.createElement('div');
    searchContainer.className = 'custom-search-container';
    searchContainer.style.cssText = `
        display: flex;
        align-items: center;
        margin: 15px 0;
        justify-content: center;
        width: 100%;
    `;

    // Tạo search input
    const customSearch = document.createElement('input');
    customSearch.type = 'text';
    customSearch.placeholder = '🔍 Tìm kiếm API theo từ khóa, endpoint, method...';
    customSearch.className = 'custom-search-input';
    customSearch.style.cssText = `
        padding: 12px 20px;
        border: 2px solid #2b6cb0;
        border-radius: 25px;
        font-size: 16px;
        width: 60%;
        max-width: 500px;
        box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
        transition: all 0.3s ease;
        outline: none;
    `;

    // Tạo nút clear search
    const clearButton = document.createElement('button');
    clearButton.innerHTML = '✕';
    clearButton.className = 'clear-search-btn';
    clearButton.style.cssText = `
        margin-left: -35px;
        background: #e53e3e;
        color: white;
        border: none;
        border-radius: 50%;
        width: 25px;
        height: 25px;
        cursor: pointer;
        font-size: 12px;
        display: none;
        transition: all 0.3s ease;
    `;

    // Tạo counter hiển thị kết quả
    const resultCounter = document.createElement('div');
    resultCounter.className = 'search-result-counter';
    resultCounter.style.cssText = `
        margin-left: 10px;
        color: #718096;
        font-size: 14px;
        font-weight: 500;
    `;

    // Thêm các element vào container
    searchContainer.appendChild(customSearch);
    searchContainer.appendChild(clearButton);
    searchContainer.appendChild(resultCounter);

    // Tìm vị trí để chèn search container
    const swaggerContainer = document.querySelector('.swagger-ui');
    if (swaggerContainer) {
        const infoSection = swaggerContainer.querySelector('.info');
        if (infoSection) {
            infoSection.insertAdjacentElement('afterend', searchContainer);
        } else {
            swaggerContainer.insertBefore(searchContainer, swaggerContainer.firstChild);
        }
    }

    // Hàm tìm kiếm nâng cao
    function performSearch(searchTerm) {
        const operations = document.querySelectorAll('.opblock');
        const tags = document.querySelectorAll('.opblock-tag');
        let visibleCount = 0;
        let totalCount = operations.length;

        if (!searchTerm.trim()) {
            // Hiển thị tất cả nếu không có từ khóa
            operations.forEach(op => {
                op.style.display = 'block';
                op.classList.remove('search-highlight');
            });
            tags.forEach(tag => {
                tag.style.display = 'block';
            });
            visibleCount = totalCount;
        } else {
            const searchLower = searchTerm.toLowerCase();
            const searchWords = searchLower.split(' ').filter(word => word.length > 0);

            operations.forEach(op => {
                const path = op.querySelector('.opblock-summary-path')?.textContent.toLowerCase() || '';
                const description = op.querySelector('.opblock-summary-description')?.textContent.toLowerCase() || '';
                const method = op.querySelector('.opblock-summary-method')?.textContent.toLowerCase() || '';
                const tag = op.querySelector('.opblock-tag')?.textContent.toLowerCase() || '';
                const parameters = Array.from(op.querySelectorAll('.parameter__name')).map(p => p.textContent.toLowerCase()).join(' ');

                // Tìm kiếm theo nhiều từ khóa
                const searchableText = `${path} ${description} ${method} ${tag} ${parameters}`;
                const isMatch = searchWords.every(word => searchableText.includes(word));

                if (isMatch) {
                    op.style.display = 'block';
                    op.classList.add('search-highlight');
                    visibleCount++;
                } else {
                    op.style.display = 'none';
                    op.classList.remove('search-highlight');
                }
            });

            // Ẩn/hiện tags dựa trên kết quả tìm kiếm
            tags.forEach(tag => {
                const tagOperations = tag.parentElement.querySelectorAll('.opblock');
                const hasVisibleOps = Array.from(tagOperations).some(op => op.style.display !== 'none');
                tag.style.display = hasVisibleOps ? 'block' : 'none';
            });
        }

        // Cập nhật counter
        resultCounter.textContent = searchTerm.trim() ? 
            `Tìm thấy ${visibleCount}/${totalCount} kết quả` : 
            `${totalCount} API endpoints`;

        // Hiển thị/ẩn nút clear
        clearButton.style.display = searchTerm.trim() ? 'block' : 'none';
    }

    // Event listeners
    customSearch.addEventListener('input', (e) => {
        performSearch(e.target.value);
    });

    customSearch.addEventListener('focus', (e) => {
        e.target.style.borderColor = '#3182ce';
        e.target.style.boxShadow = '0 0 12px rgba(49, 130, 206, 0.4)';
    });

    customSearch.addEventListener('blur', (e) => {
        e.target.style.borderColor = '#2b6cb0';
        e.target.style.boxShadow = '0 2px 8px rgba(0, 0, 0, 0.1)';
    });

    clearButton.addEventListener('click', () => {
        customSearch.value = '';
        performSearch('');
        customSearch.focus();
    });

    // Keyboard shortcuts
    customSearch.addEventListener('keydown', (e) => {
        if (e.key === 'Escape') {
            customSearch.value = '';
            performSearch('');
        }
    });

    // Khởi tạo counter
    performSearch('');
});