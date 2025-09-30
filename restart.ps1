# Supermarket API Restart Script (PowerShell)
# Tự động kill port cũ và start lại ứng dụng

Write-Host "🔄 Restarting Supermarket API..." -ForegroundColor Cyan

# Định nghĩa ports
$HTTP_PORT = 5295
$HTTPS_PORT = 7000

# Hàm kill process trên port
function Kill-Port {
    param([int]$Port)
    
    Write-Host "🔍 Checking port $Port..." -ForegroundColor Yellow
    
    try {
        # Tìm process đang sử dụng port
        $processes = Get-NetTCPConnection -LocalPort $Port -ErrorAction SilentlyContinue | 
                     Select-Object -ExpandProperty OwningProcess -Unique
        
        if ($processes) {
            Write-Host "⚠️  Found processes on port $Port: $($processes -join ', ')" -ForegroundColor Yellow
            Write-Host "🔪 Killing processes..." -ForegroundColor Red
            
            foreach ($pid in $processes) {
                try {
                    $process = Get-Process -Id $pid -ErrorAction SilentlyContinue
                    if ($process) {
                        Stop-Process -Id $pid -Force
                        Write-Host "✅ Killed process $pid on port $Port" -ForegroundColor Green
                    }
                }
                catch {
                    Write-Host "❌ Failed to kill process $pid on port $Port: $($_.Exception.Message)" -ForegroundColor Red
                }
            }
            
            # Đợi một chút để port được giải phóng
            Start-Sleep -Seconds 2
        }
        else {
            Write-Host "✅ Port $Port is free" -ForegroundColor Green
        }
    }
    catch {
        Write-Host "⚠️  Error checking port $Port: $($_.Exception.Message)" -ForegroundColor Yellow
    }
}

# Kill ports
Kill-Port -Port $HTTP_PORT
Kill-Port -Port $HTTPS_PORT

Write-Host ""
Write-Host "🚀 Starting Supermarket API..." -ForegroundColor Green
Write-Host "📱 HTTP:  http://localhost:$HTTP_PORT" -ForegroundColor Blue
Write-Host "🔒 HTTPS: https://localhost:$HTTPS_PORT" -ForegroundColor Blue
Write-Host "📚 Swagger: https://localhost:$HTTPS_PORT/swagger" -ForegroundColor Blue
Write-Host ""

# Start ứng dụng
dotnet run
