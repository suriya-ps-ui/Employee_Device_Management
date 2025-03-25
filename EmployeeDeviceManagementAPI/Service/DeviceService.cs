using Data;
using Model;
using Microsoft.EntityFrameworkCore;

namespace Services{
    public class DeviceService:IDeviceService{
        AssetManagementContext assetManagementContext;
        public DeviceService(AssetManagementContext assetManagementContext){
            this.assetManagementContext=assetManagementContext;
        }
        public async Task<List<Laptop>> GetLaptopsByEmployeeAsync(string empId){
            return await assetManagementContext.Laptops.Where(l=>l.empId==empId).ToListAsync();
        }
        public async Task<Laptop> CreateLaptopAsync(Laptop laptop){
            assetManagementContext.Laptops.Add(laptop);
            await assetManagementContext.SaveChangesAsync();
            return laptop;
        }
        public async Task UpdateLaptopAsync(string empId,string lapHostName,Laptop laptop){
            var lap=await assetManagementContext.Laptops.FirstOrDefaultAsync(l=>l.empId==empId && l.lapHostName==lapHostName);
            if(lap==null){
                throw new Exception("Laptop not found.");
            }
            lap.lapModel=laptop.lapModel;
            lap.processor=laptop.processor;
            lap.storage=laptop.storage;
            lap.ram=laptop.ram;
            lap.assignedOn=laptop.assignedOn;
            lap.status=laptop.status;
            await assetManagementContext.SaveChangesAsync();
        }
        public async Task DeleteLaptopAsync(string empId, string lapHostName){
            var laptop=await assetManagementContext.Laptops.FirstOrDefaultAsync(l=>l.empId==empId && l.lapHostName==lapHostName);
            if(laptop==null){
                throw new Exception("Laptop not found.");
            }
            assetManagementContext.Remove(laptop);
            await assetManagementContext.SaveChangesAsync();
        }
        public async Task<List<Keyboard>> GetKeyboardsByEmployeeAsync(string empId){
            return await assetManagementContext.Keyboards.Where(k=>k.empId==empId).ToListAsync();
        }
        public async Task<Keyboard> CreateKeyboardAsync(Keyboard keyboard){
            assetManagementContext.Keyboards.Add(keyboard);
            await assetManagementContext.SaveChangesAsync();
            return keyboard;
        }
        public async Task UpdateKeyboardAsync(string keyId, Keyboard keyboard){
            var key=await assetManagementContext.Keyboards.FirstOrDefaultAsync(k=>k.keyId==keyId);
            if(key==null){ 
                throw new Exception("Keyboard not found");
            }
            key.keyS_No=keyboard.keyS_No;
            key.keyBrand=keyboard.keyBrand;
            key.status=key.status;
            await assetManagementContext.SaveChangesAsync();
        }
        public async Task DeleteKeyboardAsync(string keyId){
            var keyboard=await assetManagementContext.Keyboards.FirstOrDefaultAsync(k=>k.keyId==keyId);
            if(keyboard==null){
                throw new Exception("Keyboard not found.");
            }
            assetManagementContext.Remove(keyboard);
            await assetManagementContext.SaveChangesAsync();
        }
        public async Task<List<Mouse>> GetMousesByEmployeeAsync(string empId){
            return await assetManagementContext.Mouses.Where(m=>m.empId==empId).ToListAsync();
        }
        public async Task<Mouse> CreateMouseAsync(Mouse mouse){
            assetManagementContext.Mouses.Add(mouse);
            await assetManagementContext.SaveChangesAsync();
            return mouse;
        }
        public async Task UpdateMouseAsync(string mouseId, Mouse mouse){
            var mou=await assetManagementContext.Mouses.FirstOrDefaultAsync(m=>m.mouseId==mouseId);
            if(mou==null){
                throw new Exception("Keyboard not found.");
            }
            mou.mouseS_No=mouse.mouseS_No;
            mou.mouseBrand=mouse.mouseBrand;
            mou.status=mouse.status;
            await assetManagementContext.SaveChangesAsync();
        }
        public async Task DeleteMouseAsync(string mouseId){
            var mouse=await assetManagementContext.Mouses.FirstOrDefaultAsync(m=>m.mouseId==mouseId);
            if(mouse==null){
                throw new Exception("Keyboard not found.");
            }
            assetManagementContext.Remove(mouse);
            await assetManagementContext.SaveChangesAsync();
        }
    }
}